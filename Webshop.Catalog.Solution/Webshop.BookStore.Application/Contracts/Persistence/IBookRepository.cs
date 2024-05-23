using Webshop.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Contracts.Persistence;

public interface IBookRepository : IRepository<Domain.AggregateRoots.Book>
{
    Task<IEnumerable<BookDto>> GetBooksByCategory(int categoryId);
}