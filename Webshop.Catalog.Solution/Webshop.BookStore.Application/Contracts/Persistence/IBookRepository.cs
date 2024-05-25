using Webshop.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Contracts.Persistence;

public interface IBookRepository : IRepository<Domain.AggregateRoots.Book>
{
    Task<Book[]> GetBooksByCategory(int categoryId);
    Task<Book[]> GetBooksBySeller(int sellerId);
}