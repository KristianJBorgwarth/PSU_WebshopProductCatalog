using System.Reflection;
using Webshop.Domain.Common;

namespace Webshop.BookStore.Application.Contracts.Persistence;

public interface IBookRepository
{
    Task AddBook(string title, string author, string description, decimal price, int categoryId, Guid sellerId);
    Task<Result> DeleteBook(int bookId);
    Task<Result> UpdateBook(int bookId, string title, string author, string description, decimal price, int categoryId);
    Task<Result> GetBookById(int bookId);
    Task<Result> GetAllBooks();
    Task<Result> GetBooksByCategory(int categoryId);
    Task<Result> GetBooksBySeller(Guid sellerId);
}