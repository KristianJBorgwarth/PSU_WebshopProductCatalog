using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Webshop.BookStore.Application.Features.Order.Requests;
using Webshop.BookStore.Application.Test.Integration.Utilities;
using Webshop.Bookstore.Persistence.Context;
using Xunit;

namespace Webshop.BookStore.Application.Test.Integration.Order.Endpoint
{
    public class AddOrderItemEndpointTests : IntegrationTestBase
    {
        public AddOrderItemEndpointTests(IntegrationTestFactory<Program, BookstoreDbContext> factory) : base(factory)
        {
        }

        [Fact]
        public async Task AddOrderItem_WithValidRequest_ReturnsSuccess()
        {
            // Arrange
            var customer = new Domain.AggregateRoots.BookstoreCustomer
            {
                Name = "Test Customer",
                BaseCustomeerId = 1,
                IsBuyer = true,
                IsSeller = true
            };
            db.BookstoreCustomers.Add(customer);
            await db.SaveChangesAsync();

            var savedCustomer = await db.BookstoreCustomers.FirstOrDefaultAsync(c => c.Name == "Test Customer");

            var order = new Domain.AggregateRoots.Order
            {
                BuyerId = savedCustomer.Id
            };
            db.Orders.Add(order);
            await db.SaveChangesAsync();

            var savedOrder = await db.Orders.FirstOrDefaultAsync(o => o.BuyerId == savedCustomer.Id);

            var book = new Domain.AggregateRoots.Book
            {
                Title = "Test Book",
                Author = "Test Author",
                Price = 10,
                SellerId = savedCustomer.Id,
                Description = "Test Description"
            };
            db.Books.Add(book);
            await db.SaveChangesAsync();
            var savedBook = await db.Books.FirstOrDefaultAsync(b => b.Title == "Test Book");


            var request = new AddOrderItemRequest
            {
                OrderId = savedOrder.Id,
                BookId = savedBook.Id,
                BookTitle = savedBook.Title,
                Price = savedBook.Price,
                Quantity = 2
            };

            // Act
            var response = await client.PutAsJsonAsync("api/bookstore/order", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            db.OrderItems.Count().Should().Be(1);
            db.OrderItems.First().OrderId.Should().Be(savedOrder.Id);
            db.OrderItems.First().BookId.Should().Be(savedBook.Id);
            db.OrderItems.First().Quantity.Should().Be(2);
            db.Orders.First().OrderItems.Count().Should().Be(1);
        }
    }
}
