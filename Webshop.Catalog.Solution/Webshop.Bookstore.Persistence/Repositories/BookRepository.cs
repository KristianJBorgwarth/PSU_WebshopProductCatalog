using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Data.Persistence;

namespace Webshop.Bookstore.Persistence.Repositories;

public class BookRepository : BaseRepository, IBookRepository
{
    public BookRepository(string tableName, DataContext dataContext) : base(tableName, dataContext)
    {
    }
}