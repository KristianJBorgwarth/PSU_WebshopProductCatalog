namespace Webshop.BookStore.Application.Features.Book.Requests;

public class UpdateBookRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int SellerId { get; set; }
}