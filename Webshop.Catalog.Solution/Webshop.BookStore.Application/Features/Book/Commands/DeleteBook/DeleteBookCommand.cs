using MediatR;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Commands.DeleteBook;

public class DeleteBookCommand : IRequest<Result>
{
    public int BookId { get; set; }
}