using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.BookStore.Application.Features.Book.Queries.GetBooks;
using Webshop.BookStore.Domain.AggregateRoots;

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
    public async Task GivenValidQuery_ShouldReturn_ListOfBookDtos()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1" },
            new Book { Id = 2, Title = "Book 2" }
        };
        var bookDtos = new List<BookDto>
        {
            new BookDto { Id = 1, Title = "Book 1" },
            new BookDto { Id = 2, Title = "Book 2" }
        };

        A.CallTo(() => _fakeRepository.GetAll()).Returns(books);
        A.CallTo(() => _fakeMapper.Map<List<BookDto>>(books)).Returns(bookDtos);

        // Act
        var result = await _queryHandler.Handle(new GetBooksQuery(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(bookDtos);
        A.CallTo(() => _fakeRepository.GetAll()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<List<BookDto>>(books)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GivenRepositoryThrowsException_ShouldReturnFailureResult()
    {
        // Arrange
        var exceptionMessage = "Database error";
        A.CallTo(() => _fakeRepository.GetAll()).Throws(new System.Exception(exceptionMessage));

        // Act
        var result = await _queryHandler.Handle(new GetBooksQuery(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Error.Message.Should().Be(exceptionMessage);
        A.CallTo(() => _fakeRepository.GetAll()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<List<BookDto>>(A<List<Book>>.Ignored)).MustNotHaveHappened();
    }
    [Fact]
    public async Task GivenValidQuery_WhenNoBooksExist_ShouldReturn_EmptyListOfBookDtos()
    {
        // Arrange
        var books = new List<Book>();
        var bookDtos = new List<BookDto>();

        A.CallTo(() => _fakeRepository.GetAll()).Returns(books);
        A.CallTo(() => _fakeMapper.Map<List<BookDto>>(books)).Returns(bookDtos);

        // Act
        var result = await _queryHandler.Handle(new GetBooksQuery(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Value.Should().BeEmpty();
        A.CallTo(() => _fakeRepository.GetAll()).MustHaveHappenedOnceExactly();
        A.CallTo(() => _fakeMapper.Map<List<BookDto>>(books)).MustHaveHappenedOnceExactly();
    }
}