namespace Webshop.BookStore.Application.Services.CustomerService;

public interface ICustomerService
{
    Task<CustomerDto?> GetCustomerAsync(int customerId);
}