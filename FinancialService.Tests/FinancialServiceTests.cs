using System.Globalization;
using FinanceService.Application.Commands;
using FinanceService.Application.DTOs;
using FinanceService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Controller = FinanceService.Api.Controllers.Controller;

namespace FinancialService.Tests;

public class FinancialServiceTests
{ 
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Controller _controller;

    public FinancialServiceTests()
    {
        _mediatorMock = new Mock<IMediator>();
        var loggerMock = new Mock<ILogger<Controller>>();
        _controller = new Controller(loggerMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task GetUserWithFavouriteRates_UserExists_Returns200()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedDto = new GetUserWithFavouriteRateOutDto
        {
            Id = userId,
            Name = "Pavel",
            FavouriteRates = new List<FavouriteRate>
            {
                new() { Name = "USD", Rate = 90.5m.ToString(CultureInfo.InvariantCulture) },
                new() { Name = "EUR", Rate = 98.3m.ToString(CultureInfo.InvariantCulture) },
            }
        };

        _mediatorMock
            .Setup(m => m.Send(
                It.Is<GetUserWithFavouriteRateCommand>(c => c.UserId == userId),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedDto);

        // Act
        var result = await _controller.GetUserWithFavouriteRateAsync(userId, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<GetUserWithFavouriteRateOutDto>(okResult.Value);
        Assert.Equal(userId, dto.Id);
        Assert.Equal(2, dto.FavouriteRates!.Count);
    }

    [Fact]
    public async Task GetUserWithFavouriteRates_UserNotFound_Returns404()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(
                It.IsAny<GetUserWithFavouriteRateCommand>(),
                It.IsAny<CancellationToken>()))!
            .ReturnsAsync((GetUserWithFavouriteRateOutDto?)null);

        // Act
        var result = await _controller.GetUserWithFavouriteRateAsync(userId, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetUserWithFavouriteRates_SendsCorrectUserId()
    {
        // Arrange
       var userId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetUserWithFavouriteRateCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetUserWithFavouriteRateOutDto());

        // Act
        await _controller.GetUserWithFavouriteRateAsync(userId, CancellationToken.None);

        // Assert — убеждаемся что передали правильный userId
        _mediatorMock.Verify(
            m => m.Send(
                It.Is<GetUserWithFavouriteRateCommand>(c => c.UserId == userId),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}