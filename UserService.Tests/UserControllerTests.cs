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

/// <summary>
/// Тесты для микросервиса User
/// </summary>
public class UserControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        var loggerMock = new Mock<ILogger<UserController>>();
        var mapperMock = new Mock<IMapper>();
        _controller = new UserController(_mediatorMock.Object, loggerMock.Object, mapperMock.Object);
    }

    [Fact]
    public async Task CreateUser_ValidCommand_Returns201()
    {
        // Arrange
        var command = new CreateUserDto
        {
            Name = "Pavel",
            Password = "secret123",
            Favourites = "USD,EUR"
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns((Task<object?>)Task.CompletedTask);

        // Act
        var result = await _controller.RegisterUserAsync(command, CancellationToken.None);

        // Assert
        Assert.IsType<CreatedResult>(result);
    }

    [Fact]
    public async Task CreateUser_SendsCommandToMediator()
    {
        // Arrange
        var command = new CreateUserDto
        {
            Name = "Pavel",
            Password = "secret123",
            Favourites = "USD"
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns((Task.CompletedTask as Task<object?>)!);

        // Act
        await _controller.RegisterUserAsync(command, CancellationToken.None);

        // Assert
        _mediatorMock.Verify(
            m => m.Send(command, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetUserById_UserExists_Returns200WithDto()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedDto = new GetUserByIdDto
        {
            UserId = userId
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetUserByIdQuery>(q => q.UserId == userId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        // Act
        var result = await _controller.GetUserByIdAsync(expectedDto, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<GetUserByIdOutDto>(okResult.Value);
        Assert.Equal(userId, dto.Id);
        Assert.Equal("Pavel", dto.Name);
    }

    [Fact]
    public async Task GetUserById_UserNotFound_Returns404()
    {
        // Arrange
        var userId = Guid.NewGuid();
        
        var expectedDto = new GetUserByIdDto
        {
            UserId = userId
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetUserByIdQuery>(q => q.UserId == userId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetUserByIdOutDto?)null);

        // Act
        var result = await _controller.GetUserByIdAsync(expectedDto, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateUser_ValidCommand_Returns204()
    {
        // Arrange
        var command = new CreateUserDto
        {
            Name = "Pavel Updated",
            Password = "newpass",
            Favourites = "USD"
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .Returns((Task<object?>)Task.CompletedTask);

        // Act
        var result = await _controller.UpdateUserAsync(command, CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
    [Fact]
    public async Task Login_ValidCredentials_Returns200WithAccessToken()
    {
        // Arrange
        var command = new AuthentificationUserDto()
        {
            Name = "Pavel",
            Password = "secret123"
        };

        var expectedDto = new AuthentificationUserOutDto
        {
            AccessToken = "access_token_value",
            RefreshToken = "refresh_token_value"
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        // Act
        var result = await _controller.AuthentificationUserAsync(command, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public async Task Login_InvalidCredentials_Returns401()
    {
        // Arrange
        var command = new AuthentificationUserDto
        {
            Name = "Pavel",
            Password = "wrongpassword"
        };

        _mediatorMock
            .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync((AuthentificationUserOutDto?)null);

        // Act
        var result = await _controller.AuthentificationUserAsync(command, CancellationToken.None);

        // Assert
        Assert.IsType<UnauthorizedResult>(result);
    }

    [Fact]
    public async Task Logout_WithValidCookie_Returns204()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Cookie"] = "refreshToken=some_refresh_token";
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<LogoutUserCommand>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.LogoutUserAsync(CancellationToken.None);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Logout_WithoutCookie_Returns400()
    {
        // Arrange — пустой HttpContext без cookie
        var httpContext = new DefaultHttpContext();
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        // Act
        var result = await _controller.LogoutUserAsync(CancellationToken.None);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Refresh_ValidToken_Returns200WithNewTokens()
    {
        // Arrange
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers["Cookie"] = "refreshToken=valid_refresh_token";
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        var expectedDto = new AuthentificationUserOutDto
        {
            AccessToken = "new_access_token",
            RefreshToken = "new_refresh_token"
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<RefreshTokenCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        // Act
        var result = await _controller.RefreshUserAsync(CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
}