using MediatR;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Queries.GetBooks;

public class GetBooksQuery : IRequest<Result<List<BookDto>>>
{
    
}