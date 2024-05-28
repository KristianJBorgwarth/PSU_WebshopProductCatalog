using MediatR;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<Result>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}