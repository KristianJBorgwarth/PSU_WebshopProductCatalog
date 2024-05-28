using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Order.Commands.CreateOrder;

namespace Webshop.Bookstore.Application.Test.Unit.Order.Commands;

public class CreateOrderCommandHandlerTests
{
    private readonly IOrderRepository _fakeOrderRepository;
    private readonly IMapper _fakeMapper;
    private readonly ILogger<CreateOrderCommandHandler> _fakeLogger;
    private readonly CreateOrderCommandHandler _commandHandler;

    public CreateOrderCommandHandlerTests()
    {
        _fakeOrderRepository = A.Fake<IOrderRepository>();
        _fakeMapper = A.Fake<IMapper>();
        _fakeLogger = A.Fake<ILogger<CreateOrderCommandHandler>>();
        _commandHandler = new CreateOrderCommandHandler(_fakeOrderRepository, _fakeMapper, _fakeLogger);
    }

    [Fact]
    public async Task Handle_ValidOrder_ShouldCreateOrder()
    {
        // Arrange
        var createOrderCommand = new CreateOrderCommand
        {
            BuyerId = 1,
        };
        var order = new BookStore.Domain.AggregateRoots.Order();

        A.CallTo(() => _fakeMapper.Map<BookStore.Domain.AggregateRoots.Order>(createOrderCommand)).Returns(order);
        A.CallTo(() => _fakeOrderRepository.CreateAsync(order)).Returns(Task.CompletedTask);

        // Act
        var result = await _commandHandler.Handle(createOrderCommand, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        A.CallTo(() => _fakeOrderRepository.CreateAsync(order)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<BookStore.Domain.AggregateRoots.Order>(createOrderCommand)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_InvalidOrder_ShouldReturnFailure()
    {
        // Arrange
        var createOrderCommand = new CreateOrderCommand
        {
            BuyerId = 1,
        };
        var order = new BookStore.Domain.AggregateRoots.Order();
        var exception = new Exception("Database error");

        A.CallTo(() => _fakeMapper.Map<BookStore.Domain.AggregateRoots.Order>(createOrderCommand)).Returns(order);
        A.CallTo(() => _fakeOrderRepository.CreateAsync(order)).Throws(exception);

        // Act
        var result = await _commandHandler.Handle(createOrderCommand, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Message.Should().Contain("Database error");
        A.CallTo(() => _fakeOrderRepository.CreateAsync(order)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<BookStore.Domain.AggregateRoots.Order>(createOrderCommand)).MustHaveHappenedOnceExactly();
    }
}
