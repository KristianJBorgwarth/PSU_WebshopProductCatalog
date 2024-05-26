using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Requests;
using Webshop.BookStore.Application.Test.Integration.Utilities;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.BookstoreCustomer.Endpoint;

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
        var invalidId = 696969;
        var request = new CreateBookStoreCustomerRequest()
        {
            //This will be an invalid id
            CustomerId = invalidId,
            IsSeller = true,
            IsBuyer = true,
        };

        // Act
        var response = await client.PostAsJsonAsync("api/bookstore/customer", request);
        var content = await response.Content.ReadAsStringAsync();

        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();


        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest, content);
        errorResponse.Should().NotBeNull();
        errorResponse.ErrorMessage.Should().Be("Error: An error occurred while retrieving customer with id 696969. Status code: BadRequest (unspecified.error)");
        db.BookstoreCustomers.Should().BeEmpty();
    }
}