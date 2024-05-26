using System.Data.Common;
using System.Net;
using System.Net.Http.Json;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Requests;
using Webshop.Bookstore.Persistence.Context;
using FluentAssertions;

namespace Webshop.BookStore.Application.Test.Integration.Utilities;

public class CreateCustomerEndpointTest : IntegrationTestBase
{
    public CreateCustomerEndpointTest(IntegrationTestFactory<Program, BookstoreDbContext> factory) : base(factory)
    {

    }

    [Fact]
    public async Task CreateCustomerEndpoint_ShouldReturnCreated()
    {
        // Arrange
        var request = new CreateBookStoreCustomerRequest
        {
            CustomerId = 1,
            IsSeller = true,
            IsBuyer = true
        };

        // Act
        var response = await client.PostAsJsonAsync("api/bookstore/customer", request);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK, content);
        db.BookstoreCustomers.Should().ContainSingle();
    }

    [Fact]
    public async Task CreateCustomerEndpoint_ShouldReturn400BadRequest()
    {
        // Arrange
        var request = new CreateBookStoreCustomerRequest()
        {
            //This will be an invalid id
            CustomerId = 696969,
            IsSeller = true,
            IsBuyer = true,
        };

        // Act
        var response = await client.PostAsJsonAsync("api/bookstore/customer", request);
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest, content);
        db.BookstoreCustomers.Should().BeEmpty();
    }
}
