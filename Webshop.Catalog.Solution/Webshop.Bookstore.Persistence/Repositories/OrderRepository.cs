using Microsoft.EntityFrameworkCore;
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
        var order = await _context.Orders.FindAsync(id);
        return order;
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        var orders = await _context.Orders.Include(c=> c.OrderItems).ToListAsync();
        return orders;
    }

    public async Task UpdateAsync(Order entity)
    {
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync();
    }
}