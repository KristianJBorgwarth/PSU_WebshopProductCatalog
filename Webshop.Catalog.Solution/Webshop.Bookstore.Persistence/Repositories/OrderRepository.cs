using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.Bookstore.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly BookstoreDbContext _context;

    public OrderRepository(BookstoreDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Order entity)
    {
        await _context.Orders.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Order entity)
    {
        throw new NotImplementedException();
    }
}