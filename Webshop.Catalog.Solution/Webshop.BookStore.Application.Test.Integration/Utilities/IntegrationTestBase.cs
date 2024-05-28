﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.Utilities;


[Collection("TestCollection")]
public class IntegrationTestBase : IDisposable
{
    protected readonly BookstoreDbContext db;
    protected readonly HttpClient client;

    protected IntegrationTestBase(WebApplicationFactory<Program> factory)
    {
        var scope = factory.Services.CreateScope();
        db = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
        db.Database.EnsureCreated();
        client = factory.CreateClient();
    }

    public void Dispose()
    {
        db.OrderItems.ExecuteDelete();
        db.Orders.ExecuteDelete();
        db.Books.ExecuteDelete();
        db.BookstoreCustomers.ExecuteDelete();
    }
}