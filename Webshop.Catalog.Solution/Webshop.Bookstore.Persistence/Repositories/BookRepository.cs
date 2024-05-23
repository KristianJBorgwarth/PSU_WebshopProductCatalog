using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Bookstore.Persistence.Context;
using Webshop.Data.Persistence;
using Webshop.Domain.Common;

namespace Webshop.Bookstore.Persistence.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookstoreDbContext _context;

    public BookRepository(BookstoreDbContext context)
    {
        _context = context;
    }

    public async Task AddBook(string title, string author, string description, decimal price, int categoryId, Guid sellerId)
    {
        var book = new Book()
        {
            Title = title,
            Author = author,
            Description = description,
            Price = price,
            CategoryId = categoryId,
            SellerId = sellerId
        };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task<Result> DeleteBook(int bookId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UpdateBook(int bookId, string title, string author, string description, decimal price, int categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> GetBookById(int bookId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> GetAllBooks()
    {
        throw new NotImplementedException();
    }

    public async Task<Result> GetBooksByCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> GetBooksBySeller(Guid sellerId)
    {
        throw new NotImplementedException();
    }
}