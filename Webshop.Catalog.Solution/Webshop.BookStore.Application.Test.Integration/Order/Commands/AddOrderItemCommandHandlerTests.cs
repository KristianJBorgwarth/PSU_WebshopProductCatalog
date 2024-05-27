using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Order.Commands.AddOrderItemCommand;
using Webshop.BookStore.Application.Test.Integration.Utilities;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.Order.Commands
{
    public class AddOrderItemCommandHandlerTests : IntegrationTestBase
    {
        private readonly AddOrderItemCommandHandler _commandHandler;

        public AddOrderItemCommandHandlerTests(IntegrationTestFactory<Program, BookstoreDbContext> factory) : base(factory)
        {
            var scope = factory.Services.CreateScope();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AddOrderItemCommandHandler>>();
            _commandHandler = new(orderRepository, logger);
        }

        [Fact]
        public async Task Handle_InvalidRequests_ShouldReturnFalse_AndNotAddOrderItem()
        {
            // Arrange
            var request = new AddOrderItemCommand
            {
                OrderId = -1, // Invalid OrderId
                OrderItem = new Domain.AggregateRoots.OrderItem
                {
                    BookId = 1,
                    BookTitle = "Test Book",
                    Price = 10.0m,
                    Quantity = 1
                }
            };

            // Act
            var result = await _commandHandler.Handle(request, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().NotBeNull();
            db.OrderItems.Count().Should().Be(0);
        }

        [Fact]
        public async Task Handle_ValidRequests_ShouldReturnTrue_AndAddOrderItem()
        {
            // Arrange
            db.BookstoreCustomers.Add(new Domain.AggregateRoots.BookstoreCustomer
            {
                Name = "Test Customer",
                BaseCustomeerId = 1,
                IsBuyer = true,
                IsSeller = true
            });
            await db.SaveChangesAsync();
            var customer = db.BookstoreCustomers.First();

            var order = new Domain.AggregateRoots.Order
            {
                BuyerId = customer.Id
            };
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            var savedOrder = db.Orders.First();

            var request = new AddOrderItemCommand
            {
                OrderId = savedOrder.Id,
                OrderItem = new Domain.AggregateRoots.OrderItem
                {
                    BookId = 1,
                    BookTitle = "Test Book",
                    Price = 10.0m,
                    Quantity = 1
                }
            };

            // Act
            var result = await _commandHandler.Handle(request, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            db.OrderItems.Count().Should().Be(1);
            var savedOrderItem = db.OrderItems.First();
            savedOrderItem.OrderId.Should().Be(savedOrder.Id);
            savedOrderItem.BookId.Should().Be(1);
            savedOrderItem.BookTitle.Should().Be("Test Book");
            savedOrderItem.Price.Should().Be(10.0m);
            savedOrderItem.Quantity.Should().Be(1);
        }
    }
}
