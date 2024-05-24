using AutoMapper;
using FakeItEasy;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Queries.GetBooks;

namespace Webshop.Bookstore.Application.Test.Unit.CommandHandlerTests.Books;

public class GetBooksQueryHandlerTests
{
    private readonly IBookRepository _fakeRepository;
    private readonly IMapper _fakeMapper;
    private readonly GetBooksQueryHandler _queryHandler;

    public GetBooksQueryHandlerTests()
    {
        _fakeMapper = A.Fake<IMapper>();
        _fakeRepository = A.Fake<IBookRepository>();
        _queryHandler = new(_fakeRepository, _fakeMapper);
    }

    [Fact]
    public async void GivenValidQuery_ShouldReturn_ListOfBookDtos()
    {
        
    }
}