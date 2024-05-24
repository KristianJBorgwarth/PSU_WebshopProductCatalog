using Webshop.Application.Contracts.Persistence;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Contracts.Persistence;

public interface IBookStoreCustomerRepository : IRepository<Domain.AggregateRoots.BookstoreCustomer>
{
    Task<Result> DeleteCustomer(Guid customerId);
    Task<Result> UpdateCustomer(Guid customerId, bool isSeller, bool isBuyer);
}