using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Webshop.BookStore.Application.Features.Order.Requests;
using Webshop.BookStore.Application.Test.Integration.Utilities;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.Order.Endpoint;

public class CreateOrderEndpointTests : IntegrationTestBase
{
    public CreateOrderEndpointTests(IntegrationTestFactory<Program, BookstoreDbContext> factory) : base(factory)
    {

    }

    [Fact]
    public async Task CreateOrder_WithValidRequest_ReturnsSuccess()
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

        var request = new CreateOrderRequest
        {
            BuyerId = customer.Id
        };

        // Act
        var response = await client.PostAsJsonAsync("api/bookstore/order", request);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        db.Orders.Count().Should().Be(1);
        db.Orders.First().BuyerId.Should().Be(customer.Id);
    }

    [Fact]
    public async Task CreateOrder_WithInvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        //Will fail because no Customer exists with the associated BuyerId
        var request = new CreateOrderRequest
        {
            BuyerId = 1
        };

        // Act
        var response = await client.PostAsJsonAsync("api/bookstore/order", request);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        db.Orders.Should().BeEmpty();
    }
}