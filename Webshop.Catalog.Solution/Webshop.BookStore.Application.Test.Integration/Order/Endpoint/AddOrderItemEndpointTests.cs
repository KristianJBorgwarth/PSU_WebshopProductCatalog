using Webshop.BookStore.Application.Test.Integration.Utilities;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.Order.Endpoint;

public class AddOrderItemEndpointTests : IntegrationTestBase
{
    public AddOrderItemEndpointTests(IntegrationTestFactory<Program, BookstoreDbContext> factory) : base(factory)
    {

    }

    [Fact]
    public async Task AddOrderItem_WithValidRequest_ReturnsSuccess()
    {
        // // Arrange
        // db.BookstoreCustomers.Add(new Domain.AggregateRoots.BookstoreCustomer
        // {
        //     Name = "Test Customer",
        //     BaseCustomeerId = 1,
        //     IsBuyer = true,
        //     IsSeller = true
        // });
        // await db.SaveChangesAsync();
        // var customer = db.BookstoreCustomers.First();
        //
        // db.Orders.Add(new Domain.AggregateRoots.Order
        // {
        //     BuyerId = customer.Id
        // });
        // await db.SaveChangesAsync();
        // var order = db.Orders.First();
        //
        // db.Books.Add(new Domain.AggregateRoots.Book()
        // {
        //     Name = "Test Product",
        //     Price = 10.0m
        // });
        // await db.SaveChangesAsync();
        // var product = db.Products.First();
        //
        // var request = new AddOrderItemRequest
        // {
        //     OrderId = order.Id,
        //     Quantity = 1,
        //     BookId = 1,
        //
        // };
        //
        // // Act
        // var response = await client.PostAsJsonAsync("api/bookstore/order/item", request);
        // var responseContent = await response.Content.ReadAsStringAsync();
        //
        // // Assert
        // response.StatusCode.Should().Be(HttpStatusCode.OK);
        // db.OrderItems.Count().Should().Be(1);
        // db.OrderItems.First().OrderId.Should().Be(order.Id);
        // db.OrderItems.First().ProductId.Should().Be(product.Id);
        // db.OrderItems.First().Quantity.Should().Be(1);
    }
}