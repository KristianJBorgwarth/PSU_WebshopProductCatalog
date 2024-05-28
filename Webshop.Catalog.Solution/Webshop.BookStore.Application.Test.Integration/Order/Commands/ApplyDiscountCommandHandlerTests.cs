using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Order.Commands.ProcessOrder;
using Webshop.BookStore.Application.Test.Integration.Utilities;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.Order.Commands;

public class ApplyDiscountCommandHandlerTests : IntegrationTestBase
{
    private readonly ApplyDiscountCommandHandler _commandHandler;
    public ApplyDiscountCommandHandlerTests(IntegrationTestFactory<Program, BookstoreDbContext> factory) : base(factory)
    {
        var scope = factory.Services.CreateScope();
        var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplyDiscountCommandHandler>>();
        _commandHandler = new(orderRepository, logger);
    }

    [Fact]
    public async void Handle_GivenValidCommand_ShouldApplyDiscount_ReturnOk()
    {
        //Arrange
        var bookstoreCustomer = new Domain.AggregateRoots.BookstoreCustomer
        {
            Name = "Test Customer",
            IsBuyer = true,
            IsSeller = false
        };
        db.BookstoreCustomers.Add(bookstoreCustomer);
        await db.SaveChangesAsync();

        var customerId = bookstoreCustomer.Id;

        var order = new Domain.AggregateRoots.Order
        {
            BuyerId = customerId,
            DiscountApplied = false,
            OrderDate = DateTime.Now,
            TotalAmount = 30m
        };

        db.Orders.Add(order);
        await db.SaveChangesAsync();

        var request = new ApplyDiscountCommand
        {
            OrderId = order.Id,
            Discount = 0.12m
        };

        //Act
        var result = await _commandHandler.Handle(request, CancellationToken.None);

        //Assert
        result.Success.Should().BeTrue();
        db.Orders.Count().Should().Be(1);
    }



}