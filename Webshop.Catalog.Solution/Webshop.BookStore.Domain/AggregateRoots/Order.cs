using Webshop.Domain.Common;

namespace Webshop.BookStore.Domain.AggregateRoots;

public class Order : AggregateRoot
{
    public int Id { get; set; }
    public int BuyerId { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
}