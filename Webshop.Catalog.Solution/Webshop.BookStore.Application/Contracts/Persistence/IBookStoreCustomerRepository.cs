using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Contracts.Persistence;

public interface IBookStoreCustomerRepository
{
    Task<Result> AddCustomer(int customerId, string name, bool isSeller, bool isBuyer);
    Task<Result> DeleteCustomer(Guid customerId);
    Task<Result> UpdateCustomer(Guid customerId, bool isSeller, bool isBuyer);
}