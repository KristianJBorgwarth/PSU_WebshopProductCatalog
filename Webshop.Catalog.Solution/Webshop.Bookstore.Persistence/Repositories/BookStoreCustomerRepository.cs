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

    public async Task<Result> DeleteCustomer(Guid customerId)
    {
        BookstoreCustomer? customer = await _context.BookstoreCustomers.FindAsync(customerId);
        if (customer == null) return Result.Fail(Errors.General.NotFound(customerId));
        _context.BookstoreCustomers.Remove(customer);
        await _context.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result> UpdateCustomer(Guid customerId, bool isSeller, bool isBuyer)
    {
        BookstoreCustomer? customer = _context.BookstoreCustomers.Find(customerId);
        if (customer == null)
        {
            return Result.Fail(Errors.General.NotFound(customerId));
        }
        customer.IsSeller = isSeller;
        customer.IsBuyer = isBuyer;
        await _context.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task CreateAsync(BookstoreCustomer entity)
    {
        _context.BookstoreCustomers.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BookstoreCustomer> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BookstoreCustomer>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(BookstoreCustomer entity)
    {
        throw new NotImplementedException();
    }
}