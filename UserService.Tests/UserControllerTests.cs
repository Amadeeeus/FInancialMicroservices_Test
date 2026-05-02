using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using User.UserService.Application.Commands;
using User.UserService.Application.Dtos;
using UserService.Api.Controllers;
using UserServiceApplication.Commands;
using UserServiceApplication.Dtos;
using UserServiceApplication.Queries;

namespace Microservices.Test;

public class UserControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _mapperMock = new Mock<IMapper>();
        var loggerMock = new Mock<ILogger<UserController>>();

        _controller = new UserController(
            _mediatorMock.Object,
            loggerMock.Object,
            _mapperMock.Object);

        // Дефолтный HttpContext
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
    }

    // ==================== Register ====================

    [Fact]
    public async Task Register_ValidInput_Returns201()
    {
        // Arrange
        var dto = new CreateUserDto { Name = "Pavel", Password = "secret123", Favourites = "USD" };
        var command = new CreateUserCommand { Name = "Pavel", Password = "secret123", Favourites = "USD" };

        _mapperMock
            .Setup(m => m.Map<CreateUserDto, CreateUserCommand>(dto))
            .Returns(command);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.RegisterUserAsync(dto, CancellationToken.None);

        // Assert
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task Register_SendsCommandToMediator()
    {
        // Arrange
        var dto = new CreateUserDto { Name = "Pavel", Password = "secret123" };
        var command = new CreateUserCommand { Name = "Pavel", Password = "secret123" };

        _mapperMock
            .Setup(m => m.Map<CreateUserDto, CreateUserCommand>(dto))
            .Returns(command);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _controller.RegisterUserAsync(dto, CancellationToken.None);

        // Assert
        _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
    }

    // ==================== GetUserById ====================

    [Fact]
    public async Task GetUserById_UserExists_Returns200()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedDto = new GetUserByIdOutDto(userId, "Pavel", "USD");

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetUserByIdQuery>(q => q.UserId == userId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        // Act
        var result = await _controller.GetUserByIdAsync(userId, CancellationToken.None);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<GetUserByIdOutDto>(ok.Value);
        Assert.Equal(userId, dto.Id);
    }

    [Fact]
    public async Task GetUserById_UserNotFound_Returns404()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetUserByIdQuery>(q => q.UserId == userId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetUserByIdOutDto?)null);

        // Act
        var result = await _controller.GetUserByIdAsync(userId, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    // ==================== Auth (Login) ====================

    [Fact]
    public async Task Login_ValidCredentials_Returns200WithAccessToken()
    {
        // Arrange
        var dto = new AuthentificationUserDto { Name = "Pavel", Password = "secret123" };
        var command = new AuthentificationUserCommand { Name = "Pavel", Password = "secret123" };
        var tokens = new AuthentificationUserOutDto
        {
            AccessToken = "access_token",
            RefreshToken = "refresh_token"
        };

        _mapperMock
            .Setup(m => m.Map<AuthentificationUserDto, AuthentificationUserCommand>(dto))
            .Returns(command);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tokens);

        // Act
        var result = await _controller.AuthentificationUserAsync(dto, CancellationToken.None);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(ok.Value);
    }

    [Fact]
    public async Task Login_InvalidCredentials_Returns401()
    {
        // Arrange
        var dto = new AuthentificationUserDto { Name = "Pavel", Password = "wrong" };
        var command = new AuthentificationUserCommand { Name = "Pavel", Password = "wrong" };

        _mapperMock
            .Setup(m => m.Map<AuthentificationUserDto, AuthentificationUserCommand>(dto))
            .Returns(command);

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync((AuthentificationUserOutDto?)null);

        // Act
        var result = await _controller.AuthentificationUserAsync(dto, CancellationToken.None);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    // ==================== Logout ====================

    [Fact]
    public async Task Logout_WithValidCookie_ReturnsOk()
    {
        // Arrange — устанавливаем cookie "Refresh"
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers.Cookie = "Refresh=some_refresh_token";
        _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<LogoutUserCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.LogoutUserAsync(CancellationToken.None);

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task Logout_WithoutCookie_ReturnsUnauthorized()
    {
        // Arrange — пустой контекст без cookie
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await _controller.LogoutUserAsync(CancellationToken.None);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    // ==================== Refresh ====================

    [Fact]
    public async Task Refresh_ValidCookie_Returns200()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers.Cookie = "Refresh=valid_refresh_token";
        _controller.ControllerContext = new ControllerContext { HttpContext = httpContext };

        var command = new RefreshTokenCommand { RefreshToken = "valid_refresh_token" };
        var tokens = new AuthentificationUserOutDto
        {
            AccessToken = "new_access_token",
            RefreshToken = "new_refresh_token"
        };

        _mapperMock
            .Setup(m => m.Map<RefreshTokenCommand>(It.IsAny<string>()))
            .Returns(command);

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<RefreshTokenCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tokens);

        // Act
        var result = await _controller.RefreshUserAsync(CancellationToken.None);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Refresh_WithoutCookie_ReturnsUnauthorized()
    {
        // Arrange
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        // Act
        var result = await _controller.RefreshUserAsync(CancellationToken.None);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }
}
