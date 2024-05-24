using Webshop.Domain.Common;

namespace Webshop.BookStore.Domain.AggregateRoots;

public class Book : AggregateRoot
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }

    public BookstoreCustomer Seller { get; set; }
}