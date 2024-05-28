using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;
using Webshop.BookStore.Application.Services.PaymentService;
using Webshop.BookStore.Application.Test.Integration.Utilities;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Bookstore.Persistence.Context;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Test.Integration.Order.Commands;

public class ProcessOrderCommandHandlerTests : IntegrationTestBase
{
    private readonly ProcessOrderCommandHandler _commandHandler;
    public ProcessOrderCommandHandlerTests(IntegrationTestFactory<Program, BookstoreDbContext> factory) : base(factory)
    {
        var scope = factory.Services.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ProcessOrderCommandHandler>>();
        var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
        _commandHandler = new(paymentService, repo, logger);
    }

    [Fact]
    public async Task Handle_ValidOrder_ShouldReturnSuccess()
    {
        // Arrange
        var customer = new Domain.AggregateRoots.BookstoreCustomer()
        {
            BaseCustomeerId = 1,
            IsBuyer = true,
            IsSeller = true,
            Name = "Test Customer"
        };

        var cm= db.BookstoreCustomers.Add(customer);
        await db.SaveChangesAsync();
        var order = new Domain.AggregateRoots.Order
        {
            BuyerId = cm.Entity.Id,
            Status = OrderStatus.Pending
        };

        var o = db.Add(order);
        await db.SaveChangesAsync();

        var command = new ProcessOrderCommand()
        {
            OrderId = o.Entity.Id,
            PaymentDetails = new ()
            {
                CardNumber = "1234567890123456",
                Amount = 100,
                CVC = 123,
                ExpirationDate = "12/23"
            }
        };

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_InvalidOrder_ShouldReturnFailure()
    {
        // Arrange
        var customer = new Domain.AggregateRoots.BookstoreCustomer()
        {
            BaseCustomeerId = 1,
            IsBuyer = true,
            IsSeller = true,
            Name = "Test Customer"
        };

        var cm= db.BookstoreCustomers.Add(customer);
        await db.SaveChangesAsync();
        var order = new Domain.AggregateRoots.Order
        {
            BuyerId = cm.Entity.Id,
            Status = OrderStatus.Completed
        };

        var o = db.Add(order);
        await db.SaveChangesAsync();

        var command = new ProcessOrderCommand()
        {
            OrderId = o.Entity.Id,
            PaymentDetails = new ()
            {
                CardNumber = "1234567890123456",
                Amount = 100,
                CVC = 123,
                ExpirationDate = "12/23"
            }
        };

        // Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
    }
}