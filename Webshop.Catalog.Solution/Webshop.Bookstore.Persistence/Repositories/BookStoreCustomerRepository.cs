using Microsoft.EntityFrameworkCore;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.Bookstore.Persistence.Repositories;

public class BookStoreCustomerRepository : IBookStoreCustomerRepository
{
    private readonly BookstoreDbContext _context;
    public BookStoreCustomerRepository(BookstoreDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(BookstoreCustomer entity)
    {
        _context.BookstoreCustomers.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _context.BookstoreCustomers.Where(c => c.Id == id).ExecuteDeleteAsync();
    }

    public async Task<BookstoreCustomer> GetById(int id)
    {
        var cm = await _context.BookstoreCustomers.FindAsync(id);
        return cm;
    }

    public async Task<IEnumerable<BookstoreCustomer>> GetAll()
    {
        var customers = await _context.BookstoreCustomers.ToArrayAsync();
        return customers;
    }

    public async Task UpdateAsync(BookstoreCustomer entity)
    {
        _context.BookstoreCustomers.Update(entity);
        await _context.SaveChangesAsync();
    }
}