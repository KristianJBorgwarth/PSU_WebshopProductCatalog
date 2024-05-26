using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.Utilities;


[Collection("TestCollection")]
public class IntegrationTestBase : IDisposable
{
    protected IntegrationTestFactory<Program, BookstoreDbContext> Factory { get; private set; }
    protected BookstoreDbContext Db { get; private set; }

    public IntegrationTestBase(IntegrationTestFactory<Program, BookstoreDbContext> factory)
    {
        Factory = factory;
        var scope = factory.Services.CreateScope();
        Db = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
        Db.Database.EnsureCreated();
    }

    public virtual void Dispose()
    {
        Db.BookstoreCustomers.ExecuteDelete();
        Db.Books.ExecuteDelete();
        Db.Orders.ExecuteDelete();
        Db.OrderItems.ExecuteDelete();
    }
}