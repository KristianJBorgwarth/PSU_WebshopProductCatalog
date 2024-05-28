using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;

namespace Webshop.Bookstore.Application.Test.Unit.Order.Commands;

public class ApplyDiscountCommandHandlerTests
{
    private readonly IOrderRepository _fakeOrderRepository;
    private readonly ILogger<ApplyDiscountCommandHandler> _fakeLogger;

    public ApplyDiscountCommandHandlerTests()
    {
        _fakeOrderRepository = A.Fake<IOrderRepository>();
        _fakeLogger = A.Fake<ILogger<ApplyDiscountCommandHandler>>();
    }

    [Fact]
    public async void Given_ValidOrder_When_ApplyDiscountCommandCalled_Then_ShouldApplyDiscount()
    {
        // Arrange
        var handler = new ApplyDiscountCommandHandler(_fakeOrderRepository, _fakeLogger);
        var discount = 0.10m;
        var orderId = 1;
        var command = new ApplyDiscountCommand() {Discount = discount, OrderId = orderId};
        var order = new BookStore.Domain.AggregateRoots.Order { Id = orderId, TotalAmount = 100};
        A.CallTo(() => _fakeOrderRepository.GetById(orderId)).Returns(order);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        A.CallTo(() => _fakeOrderRepository.UpdateAsync(order)).MustHaveHappened();
        result.Success.Should().BeTrue();
        order.TotalAmount.Should().Be(90);
    }

    [Fact]
    public async void Given_InvalidDiscount_When_ApplyDiscountCommandCalled_Then_ShouldReturnFailure()
    {
        // Arrange
        var handler = new ApplyDiscountCommandHandler(_fakeOrderRepository, _fakeLogger);
        var discount = 0.20m; // Invalid discount
        var orderId = 1;
        var command = new ApplyDiscountCommand() { Discount = discount, OrderId = orderId };
        var order = new BookStore.Domain.AggregateRoots.Order { Id = orderId, TotalAmount = 100 };
        A.CallTo(() => _fakeOrderRepository.GetById(orderId)).Returns(order);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Message.Should().Be("Discount must be between 0 and 15%");
    }

    [Fact]
    public async void Given_OrderWithDiscount_When_ApplyDiscountCommandCalled_Then_ShouldReturnFailure()
    {
        // Arrange
        var handler = new ApplyDiscountCommandHandler(_fakeOrderRepository, _fakeLogger);
        var discount = 0.10m;
        var orderId = 1;
        var command = new ApplyDiscountCommand() { Discount = discount, OrderId = orderId };
        var order = new BookStore.Domain.AggregateRoots.Order { Id = orderId, TotalAmount = 100, DiscountApplied = true };
        A.CallTo(() => _fakeOrderRepository.GetById(orderId)).Returns(order);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Message.Should().Be("Discount already applied to order");
    }

    [Fact]
    public async void Given_NonExistentOrder_When_ApplyDiscountCommandCalled_Then_ShouldReturnFailure()
    {
        // Arrange
        var handler = new ApplyDiscountCommandHandler(_fakeOrderRepository, _fakeLogger);
        var discount = 0.10m;
        var orderId = 999; // Non-existent order ID
        var command = new ApplyDiscountCommand() { Discount = discount, OrderId = orderId };
        A.CallTo(() => _fakeOrderRepository.GetById(orderId)).Returns((BookStore.Domain.AggregateRoots.Order)null);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Message.Should().Be($"Could not find entity with ID 999.");
    }

    [Fact]
    public async void Given_RepositoryThrowsException_When_ApplyDiscountCommandCalled_Then_ReturnFailure()
    {
        // Arrange
        var handler = new ApplyDiscountCommandHandler(_fakeOrderRepository, _fakeLogger);
        var discount = 0.10m;
        var orderId = 1;
        var command = new ApplyDiscountCommand() { Discount = discount, OrderId = orderId };
        var order = new BookStore.Domain.AggregateRoots.Order { Id = orderId, TotalAmount = 100 };

        var exception = new Exception("Database error");
        A.CallTo(() => _fakeOrderRepository.GetById(orderId)).Throws(exception);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Message.Should().Be("Database error");
    }
}