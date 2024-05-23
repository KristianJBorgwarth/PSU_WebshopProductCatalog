using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Services.CustomerService;

public interface ICustomerService
{
    Task<Result<CustomerResult>> GetCustomerAsync(int customerId);
}