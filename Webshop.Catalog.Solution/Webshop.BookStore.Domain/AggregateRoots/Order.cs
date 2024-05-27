using Webshop.Domain.Common;

namespace Webshop.BookStore.Domain.AggregateRoots;

public class Order : AggregateRoot
{
    public int BuyerId { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }

    public void AddItem(OrderItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        OrderItems.Add(item);
        CalculateTotalAmount();
    }

    private void CalculateTotalAmount()
    {
        TotalAmount = OrderItems.Sum(x => x.Price * x.Quantity);
    }
}