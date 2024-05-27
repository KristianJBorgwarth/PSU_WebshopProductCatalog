using Webshop.Domain.Common;

namespace Webshop.BookStore.Domain.AggregateRoots;

public class OrderItem : AggregateRoot
{
    public int OrderId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}