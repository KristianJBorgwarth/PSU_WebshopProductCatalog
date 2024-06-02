using Webshop.BookStore.Domain.AggregateRoots;

namespace Webshop.BookStore.Application.Features.Order.Dtos
{
    public class OrderDto
    {
        public int BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public bool DiscountApplied { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class OrderItemDto
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}