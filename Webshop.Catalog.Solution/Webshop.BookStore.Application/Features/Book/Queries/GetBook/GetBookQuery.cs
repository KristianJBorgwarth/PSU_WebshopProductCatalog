using MediatR;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Queries.GetBook;

public class GetBookQuery : IRequest<Result<BookDto>>
{
    public int BookId { get; set; }
}