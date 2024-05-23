using MediatR;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Features.Book.Queries.GetBooksByCategory;

public class GetBooksByCategoryQueryHandler : IRequestHandler<GetBooksByCategoryQuery, Result<List<BookDto>>>
{
    public async Task<Result<List<BookDto>>> Handle(GetBooksByCategoryQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}