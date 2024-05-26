using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Dtos;
using Webshop.BookStore.Application.Features.Book.Queries.GetBooksBySeller;
using Webshop.BookStore.Domain.AggregateRoots;

namespace Webshop.Bookstore.Application.Test.Unit.QueryHandlerTests.Books;

public class GetBooksBySellerQueryHandlerTests
{
    private readonly GetBooksBySellerQueryHandler _queryHandler;
    private readonly IBookRepository _fakeBookRepository;
    private readonly IMapper _fakeMapper;

    public GetBooksBySellerQueryHandlerTests()
    {
        _fakeBookRepository = A.Fake<IBookRepository>();
        _fakeMapper = A.Fake<IMapper>();
        _queryHandler = new(_fakeBookRepository, _fakeMapper);
    }

     [Fact]
        public async Task Handle_GivenValidSellerId_ShouldReturnBooks()
        {
            // Arrange
            var query = new GetBooksBySellerQuery { SellerId = 1 };
            var books = new Book[]
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", Description = "Description 1", Price = 10, CategoryId = 1, SellerId = 1 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", Description = "Description 2", Price = 20, CategoryId = 1, SellerId = 1 }
            };
            var bookDtos = new List<BookDto>
            {
                new BookDto { Id = 1, Title = "Book 1", Author = "Author 1", Description = "Description 1", Price = 10, CategoryId = 1, SellerId = 1 },
                new BookDto { Id = 2, Title = "Book 2", Author = "Author 2", Description = "Description 2", Price = 20, CategoryId = 1, SellerId = 1 }
            };

            A.CallTo(() => _fakeBookRepository.GetBooksBySeller(query.SellerId)).Returns(books);
            A.CallTo(() => _fakeMapper.Map<List<BookDto>>(books)).Returns(bookDtos);

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(bookDtos);
            A.CallTo(() => _fakeBookRepository.GetBooksBySeller(query.SellerId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeMapper.Map<List<BookDto>>(books)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_GivenValidSellerId_ShouldReturnEmptyList()
        {
            // Arrange
            var query = new GetBooksBySellerQuery { SellerId = 1 };
            var books = Array.Empty<Book>();
            var bookDtos = new List<BookDto>();

            A.CallTo(() => _fakeBookRepository.GetBooksBySeller(query.SellerId)).Returns(books);
            A.CallTo(() => _fakeMapper.Map<List<BookDto>>(books)).Returns(bookDtos);

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.Value.Should().BeEmpty();
            A.CallTo(() => _fakeBookRepository.GetBooksBySeller(query.SellerId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeMapper.Map<List<BookDto>>(books)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Handle_WhenExceptionThrown_ShouldReturnFailureResult()
        {
            // Arrange
            var query = new GetBooksBySellerQuery { SellerId = 1 };
            var exceptionMessage = "Database connection failed";

            A.CallTo(() => _fakeBookRepository.GetBooksBySeller(query.SellerId)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await _queryHandler.Handle(query, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain(exceptionMessage);
            A.CallTo(() => _fakeBookRepository.GetBooksBySeller(query.SellerId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeMapper.Map<List<BookDto>>(A<IEnumerable<Book>>._)).MustNotHaveHappened();
        }
}