using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Contracts.Persistence;

public interface IBookStoreCustomerRepository
{
    Task<Result> AddCustomer(int customerId, bool isSeller, bool isBuyer);
    Task<Result> DeleteCustomer(int customerId);
    Task<Result> UpdateCustomer(int customerId, bool isSeller, bool isBuyer);
}