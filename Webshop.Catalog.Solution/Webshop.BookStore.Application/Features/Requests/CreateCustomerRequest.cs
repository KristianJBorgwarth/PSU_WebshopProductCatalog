namespace Webshop.BookStore.Application.Features.Requests;

public class CreateCustomerRequest
{
    public required int CustomerId { get; set; }
    public bool IsSeller { get; set; }
    public bool IsBuyer { get; set; }
}