using System.Data.Common;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Order.Commands.CreateOrder;
using Webshop.BookStore.Application.Test.Integration.Utilities;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.Order.Commands;

public class CreateOrderCommandHandlerTests : IntegrationTestBase
{
    private readonly CreateOrderCommandHandler _commandHandler;

    public CreateOrderCommandHandlerTests(IntegrationTestFactory<Program, BookstoreDbContext> factory) : base(factory)
    {
        var scope = factory.Services.CreateScope();
        var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<CreateOrderCommandHandler>>();
        _commandHandler = new(orderRepository, mapper, logger);
    }

    [Fact]
    public async Task Handle_InvalidRequests_ShouldReturnFalse_AndNotAddOrder()
    {
        // Arrange
        //WIll be invalid as no Buyer with Id 1 exists
        var request = new CreateOrderCommand
        {
            BuyerId = 1,
        };

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Message.Should().NotBeNull();
        db.Orders.Count().Should().Be(0);
    }

    [Fact]
    public async Task Handle_ValidRequests_ShouldReturnTrue_AndAddOrder()
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

        var request = new CreateOrderCommand
        {
            BuyerId = customer.Id
        };

        // Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        db.Orders.Count().Should().Be(1);
        db.Orders.First().BuyerId.Should().Be(customer.Id);
    }
}