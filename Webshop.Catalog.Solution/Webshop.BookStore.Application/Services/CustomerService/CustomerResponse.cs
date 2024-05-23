namespace Webshop.BookStore.Application.Services.CustomerService;

public class CustomerResponse
{
    public CustomerResult? Result { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime TimeGenerated { get; set; }
}

public class CustomerResult
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Email { get; set; }
}