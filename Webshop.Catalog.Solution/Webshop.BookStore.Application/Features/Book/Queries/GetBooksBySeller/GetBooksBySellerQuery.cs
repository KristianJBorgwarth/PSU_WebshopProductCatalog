using MediatR;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Queries.GetBooksBySeller;

public class GetBooksBySellerQuery : IRequest<Result<List<BookDto>>>
{
    public int SellerId { get; set; }
}