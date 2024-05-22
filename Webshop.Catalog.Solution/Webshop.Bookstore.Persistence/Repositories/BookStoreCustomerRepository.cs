using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Bookstore.Persistence.Context;
using Webshop.Domain.Common;

namespace Webshop.Bookstore.Persistence.Repositories;

public class BookStoreCustomerRepository : IBookStoreCustomerRepository
{
    private readonly BookstoreDbContext _context;
    public BookStoreCustomerRepository(BookstoreDbContext context)
    {
        _context = context;
    }

    public async Task<Result> AddCustomer(int customerId, bool isSeller, bool isBuyer)
    {
        BookstoreCustomer customer = new()
        {
            Id = customerId,
            IsSeller = isSeller,
            IsBuyer = isBuyer
        };

        _context.Add(customer);
        await _context.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result> DeleteCustomer(int customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UpdateCustomer(int customerId, bool isSeller, bool isBuyer)
    {
        throw new NotImplementedException();
    }
}