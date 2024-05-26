namespace Webshop.BookStore.Application.Features.BookStoreCustomer.Requests
{
    public class UpdateCustomerRequest
    {
        public int Id { get; set; }
        public bool IsSeller { get; set; }
        public bool IsBuyer { get; set; }
    }
}
