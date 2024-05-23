using System.Net;
using System.Text.Json;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Services.CustomerService;

public class CustomerService : ICustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CustomerServiceClient");
    }

    public async Task<Result<CustomerResult>> GetCustomerAsync(int customerId)
    {
        string url = $"{customerId}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return Result.Fail<CustomerResult>(
                    Errors.General.UnspecifiedError($"Error: An error occurred while retrieving customer with id {customerId}. Status code: {response.StatusCode}"));
            }

            string responseContent = await response.Content.ReadAsStringAsync();

            CustomerResponse customer = JsonSerializer.Deserialize<CustomerResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;

            return customer.Result!;
        }
        catch (Exception ex)
        {
            return Result.Fail<CustomerResult>(Errors.General.UnspecifiedError(ex.Message));
        }
    }
}
