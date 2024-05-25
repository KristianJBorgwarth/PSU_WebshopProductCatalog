using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.BookStore.Application.Features.Book.Queries.GetBook;
using Webshop.BookStore.Domain.AggregateRoots;
using Webshop.Domain.Common;

namespace Webshop.Bookstore.Application.Test.Unit.CommandHandlerTests.Books;

public class GetBookQueryHandlerTests
{
    private readonly GetBookQueryHandler _queryHandler;
    private readonly IMapper _fakeMapper;
    private readonly IBookRepository _fakeBookRepository;

    public GetBookQueryHandlerTests()
    {
        _fakeMapper = A.Fake<IMapper>();
        _fakeBookRepository = A.Fake<IBookRepository>();
        _queryHandler = new GetBookQueryHandler(_fakeBookRepository, _fakeMapper);
    }

    [Fact]
    public async void GetBookQueryHandler_ReturnsBookDto_WhenBookExists()
    {
        var bookId = 2;
        var book = new Book
        {
            Id = bookId,
            Title = "Test Book",
            Author = "Test Author",
            Price = 9.99m,
        };

        var bookDto = new BookDto
        {
            Id = bookId,
            Title = "Test Book",
            Author = "Test Author",
            Price = 9.99m,
        };

        A.CallTo(() => _fakeBookRepository.GetById(bookId)).Returns(book);
        A.CallTo(() => _fakeMapper.Map<BookDto>(book)).Returns(bookDto);

        var result = await _queryHandler.Handle(new GetBookQuery { BookId = bookId }, CancellationToken.None);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(bookDto);
        A.CallTo(()=> _fakeBookRepository.GetById(bookId)).MustHaveHappenedOnceExactly();
        A.CallTo(()=> _fakeMapper.Map<BookDto>(book)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetBookQueryHandler_ReturnsNotFound_WhenBookDoesNotExist()
    {
        var bookId = 2;
        A.CallTo(() => _fakeBookRepository.GetById(bookId)).Returns((Book)null);

        var result = await _queryHandler.Handle(new GetBookQuery { BookId = bookId }, CancellationToken.None);

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Error.Code.Should().Be(Errors.General.NotFound(bookId).Code);
        A.CallTo(() => _fakeBookRepository.GetById(bookId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<BookDto>(A<Book>.Ignored)).MustNotHaveHappened();
    }

    [Fact]
    public async Task GetBookQueryHandler_ReturnsFailure_WhenRepositoryThrowsException()
    {
        var bookId = 2;
        var exceptionMessage = "Database error";
        A.CallTo(() => _fakeBookRepository.GetById(bookId)).Throws(new System.Exception(exceptionMessage));

        var result = await _queryHandler.Handle(new GetBookQuery { BookId = bookId }, CancellationToken.None);

        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Error.Code.Should().Be(Errors.General.UnspecifiedError(exceptionMessage).Code);
        A.CallTo(() => _fakeBookRepository.GetById(bookId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<BookDto>(A<Book>.Ignored)).MustNotHaveHappened();
    }
}