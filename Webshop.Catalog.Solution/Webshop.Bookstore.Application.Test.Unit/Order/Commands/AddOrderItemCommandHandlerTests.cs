using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Order.Commands.AddOrderItemCommand;
using Webshop.BookStore.Domain.AggregateRoots;


namespace Webshop.Bookstore.Application.Test.Unit.Order.Commands
{
    public class AddOrderItemCommandHandlerTests
    {
        private readonly IOrderRepository _fakeOrderRepository;
        private readonly ILogger<AddOrderItemCommandHandler> _fakeLogger;
        private readonly AddOrderItemCommandHandler _commandHandler;

        public AddOrderItemCommandHandlerTests()
        {
            _fakeOrderRepository = A.Fake<IOrderRepository>();
            _fakeLogger = A.Fake<ILogger<AddOrderItemCommandHandler>>();
            _commandHandler = new AddOrderItemCommandHandler(_fakeOrderRepository, _fakeLogger);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldAddOrderItem()
        {
            // Arrange
            var order = new BookStore.Domain.AggregateRoots.Order();
            var addOrderItemCommand = new AddOrderItemCommand
            {
                OrderId = 1,
                OrderItem = new OrderItem
                {
                    BookId = 1,
                    BookTitle = "Test Book",
                    Price = 10.0m,
                    Quantity = 1
                }
            };

            A.CallTo(() => _fakeOrderRepository.GetById(addOrderItemCommand.OrderId)).Returns(order);
            A.CallTo(() => _fakeOrderRepository.UpdateAsync(order)).Returns(Task.CompletedTask);

            // Act
            var result = await _commandHandler.Handle(addOrderItemCommand, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            A.CallTo(() => _fakeOrderRepository.GetById(addOrderItemCommand.OrderId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeOrderRepository.UpdateAsync(order)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_InvalidOrder_ShouldReturnFailure()
        {
            // Arrange
            var addOrderItemCommand = new AddOrderItemCommand
            {
                OrderId = -1, // Invalid OrderId
                OrderItem = new OrderItem
                {
                    BookId = 1,
                    BookTitle = "Test Book",
                    Price = 10.0m,
                    Quantity = 1
                }
            };

            A.CallTo(() => _fakeOrderRepository.GetById(addOrderItemCommand.OrderId)).Returns<BookStore.Domain.AggregateRoots.Order>(null);

            // Act
            var result = await _commandHandler.Handle(addOrderItemCommand, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain("Could not find entity with ID -1.");
            A.CallTo(() => _fakeOrderRepository.GetById(addOrderItemCommand.OrderId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeOrderRepository.UpdateAsync(A<BookStore.Domain.AggregateRoots.Order>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Handle_RepositoryUpdateFails_ShouldReturnFailure()
        {
            // Arrange
            var order = new BookStore.Domain.AggregateRoots.Order();
            var addOrderItemCommand = new AddOrderItemCommand
            {
                OrderId = 1,
                OrderItem = new OrderItem
                {
                    BookId = 1,
                    BookTitle = "Test Book",
                    Price = 10.0m,
                    Quantity = 1
                }
            };
            var exception = new Exception("Database error");

            A.CallTo(() => _fakeOrderRepository.GetById(addOrderItemCommand.OrderId)).Returns(order);
            A.CallTo(() => _fakeOrderRepository.UpdateAsync(order)).Throws(exception);

            // Act
            var result = await _commandHandler.Handle(addOrderItemCommand, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain("Database error");
            A.CallTo(() => _fakeOrderRepository.GetById(addOrderItemCommand.OrderId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeOrderRepository.UpdateAsync(order)).MustHaveHappenedOnceExactly();
        }
    }
}
