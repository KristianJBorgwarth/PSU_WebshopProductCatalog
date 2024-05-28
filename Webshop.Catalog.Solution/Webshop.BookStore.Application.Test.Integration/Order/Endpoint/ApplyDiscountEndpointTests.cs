using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Webshop.BookStore.Application.Features.Order.Requests;
using Webshop.BookStore.Application.Test.Integration.Utilities;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.Order.Endpoint;

public class ApplyDiscountEndpointTests : IntegrationTestBase
{
    public ApplyDiscountEndpointTests(IntegrationTestFactory<Program, BookstoreDbContext> factory) : base(factory)
    {

    }

    [Fact]
    public async void GivenValidRequest_ShouldReturn200Ok()
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

        // Retrieve the customer Id
        var customerId = bookstoreCustomer.Id;

        var order = new Domain.AggregateRoots.Order
        {
            TotalAmount = 100,
            BuyerId = customerId,  // Set to valid customer Id
            DiscountApplied = false
        };
        db.Orders.Add(order);
        await db.SaveChangesAsync();

        var command = new ApplyDiscountRequest()
        {
            OrderId = order.Id,  // Use the correct order Id
            Discount = 0.12m,

        };

        //Act
        var response = await client.PutAsJsonAsync("api/bookstore/order/discount", command);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        db.Orders.Count().Should().Be(1);
        db.Orders.First().TotalAmount.Should().NotBe(100);
    }
}