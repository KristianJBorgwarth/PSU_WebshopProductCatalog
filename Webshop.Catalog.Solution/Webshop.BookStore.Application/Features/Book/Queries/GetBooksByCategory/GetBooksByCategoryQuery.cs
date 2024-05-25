using MediatR;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Queries.GetBooksByCategory;

public class GetBooksByCategoryQuery : IRequest<Result<List<BookDto>>>
{
    public int CategoryId { get; set; }
}