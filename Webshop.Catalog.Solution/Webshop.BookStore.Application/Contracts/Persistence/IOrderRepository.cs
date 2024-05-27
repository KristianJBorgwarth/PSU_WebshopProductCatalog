using Webshop.Application.Contracts.Persistence;
using Webshop.BookStore.Domain.AggregateRoots;

namespace Webshop.BookStore.Application.Contracts.Persistence;

public interface IOrderRepository : IRepository<Order>
{

}