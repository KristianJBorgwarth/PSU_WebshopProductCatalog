using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;
using Webshop.BookStore.Application.Services.PaymentService;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Domain.Common;
using Xunit;

namespace Webshop.Bookstore.Application.Test.Unit.Order.Commands
{
    public class ProcessOrderCommandHandlerTests
    {
        private readonly IOrderRepository _fakeOrderRepository;
        private readonly ILogger<ProcessOrderCommandHandler> _fakeLogger;
        private readonly IPaymentService _fakePaymentService;
        private readonly ProcessOrderCommandHandler _commandHandler;

        public ProcessOrderCommandHandlerTests()
        {
            _fakeOrderRepository = A.Fake<IOrderRepository>();
            _fakeLogger = A.Fake<ILogger<ProcessOrderCommandHandler>>();
            _fakePaymentService = A.Fake<IPaymentService>();
            _commandHandler = new ProcessOrderCommandHandler(_fakePaymentService, _fakeOrderRepository, _fakeLogger);
        }

        [Fact]
        public async Task Handle_ValidOrderCommand_ShouldReturnSuccess()
        {
            // Arrange
            var order = new BookStore.Domain.AggregateRoots.Order
            {
                BuyerId = 1,
                Status = OrderStatus.Pending
            };
            var cmd = new ProcessOrderCommand
            {
                OrderId = 1,
                PaymentDetails = new PaymentRequest
                {
                    Amount = 100,
                    CardNumber = "1234567890123456",
                    CVC = 123,
                    ExpirationDate = "12/23"
                }
            };

            A.CallTo(() => _fakeOrderRepository.GetById(1)).Returns(order);
            A.CallTo(() => _fakePaymentService.ProcessPayment(cmd.PaymentDetails)).Returns(Result.Ok());

            // Act
            var result = await _commandHandler.Handle(cmd, new CancellationToken());

            // Assert
            result.Success.Should().BeTrue();
            order.Status.Should().Be(OrderStatus.Completed);
        }

        [Fact]
        public async Task Handle_OrderNotFound_ShouldReturnFailure()
        {
            // Arrange
            var cmd = new ProcessOrderCommand
            {
                OrderId = 1,
                PaymentDetails = new PaymentRequest
                {
                    Amount = 100,
                    CardNumber = "1234567890123456",
                    CVC = 123,
                    ExpirationDate = "12/23"
                }
            };

            A.CallTo(() => _fakeOrderRepository.GetById(1)).Returns<BookStore.Domain.AggregateRoots.Order>(null);

            // Act
            var result = await _commandHandler.Handle(cmd, new CancellationToken());

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain("Could not find entity with ID 1.");
        }

        [Fact]
        public async Task Handle_InvalidOrderState_ShouldReturnFailure()
        {
            // Arrange
            var order = new BookStore.Domain.AggregateRoots.Order
            {
                BuyerId = 1,
                Status = OrderStatus.Completed // Order is already completed
            };
            var cmd = new ProcessOrderCommand
            {
                OrderId = 1,
                PaymentDetails = new PaymentRequest
                {
                    Amount = 100,
                    CardNumber = "1234567890123456",
                    CVC = 123,
                    ExpirationDate = "12/23"
                }
            };

            A.CallTo(() => _fakeOrderRepository.GetById(1)).Returns(order);

            // Act
            var result = await _commandHandler.Handle(cmd, new CancellationToken());

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain("Order cannot be processed.");
        }

        [Fact]
        public async Task Handle_FailedPayment_ShouldReturnFailure()
        {
            // Arrange
            var order = new BookStore.Domain.AggregateRoots.Order
            {
                BuyerId = 1,
                Status = OrderStatus.Pending
            };
            var cmd = new ProcessOrderCommand
            {
                OrderId = 1,
                PaymentDetails = new PaymentRequest
                {
                    Amount = 100,
                    CardNumber = "1234567890123456",
                    CVC = 123,
                    ExpirationDate = "12/23"
                }
            };

            A.CallTo(() => _fakeOrderRepository.GetById(1)).Returns(order);
            A.CallTo(() => _fakePaymentService.ProcessPayment(cmd.PaymentDetails)).Returns(Result.Fail(Errors.General.UnspecifiedError("Payment failed.")));

            // Act
            var result = await _commandHandler.Handle(cmd, new CancellationToken());

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain("An error occurred while processing the payment.");
            order.Status.Should().Be(OrderStatus.Failed);
        }

        [Fact]
        public async Task Handle_ExceptionDuringProcessing_ShouldReturnFailure()
        {
            // Arrange
            var order = new BookStore.Domain.AggregateRoots.Order
            {
                BuyerId = 1,
                Status = OrderStatus.Pending
            };
            var cmd = new ProcessOrderCommand
            {
                OrderId = 1,
                PaymentDetails = new PaymentRequest
                {
                    Amount = 100,
                    CardNumber = "1234567890123456",
                    CVC = 123,
                    ExpirationDate = "12/23"
                }
            };

            A.CallTo(() => _fakeOrderRepository.GetById(1)).Returns(order);
            A.CallTo(() => _fakePaymentService.ProcessPayment(cmd.PaymentDetails)).Throws(new Exception("Payment service error"));

            // Act
            var result = await _commandHandler.Handle(cmd, new CancellationToken());

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain("An error occurred while processing the order.");
        }
    }
}
