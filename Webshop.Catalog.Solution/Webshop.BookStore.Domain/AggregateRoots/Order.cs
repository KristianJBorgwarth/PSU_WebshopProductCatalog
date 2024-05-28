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

    /// <summary>
    /// Business logic to apply discount to the order
    /// </summary>
    /// <param name="discount">discount applied to order total</param>
    /// <exception cref="ArgumentException">exception thrown with invalid discount</exception>
    public void ApplyDiscount(decimal discount)
    {
        if (discount is < 0 or > 0.15m)
        {
            throw new ArgumentException("Discount must be between 0 and 15%");
        }
        TotalAmount -= TotalAmount * discount;
    }

    private void CalculateTotalAmount()
    {
        TotalAmount = OrderItems.Sum(x => x.Price * x.Quantity);
    }
}