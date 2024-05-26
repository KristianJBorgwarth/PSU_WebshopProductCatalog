using FakeItEasy;
using FluentAssertions;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Commands.DeleteBook;
using Webshop.BookStore.Domain.AggregateRoots;

namespace Webshop.Bookstore.Application.Test.Unit.CommandHandlerTests.Books;

public class DeleteBookCommandHandlerTests
{
    private readonly DeleteBookCommandHandler _commandHandler;
    private readonly IBookRepository _bookRepository;

    public DeleteBookCommandHandlerTests()
    {
        _bookRepository = A.Fake<IBookRepository>();
        _commandHandler = new (_bookRepository);
    }

    [Fact]
    public async void GivenValidID_ShouldDeleteBook_ReturnSuccess()
    {
        //Arrange
        var command = new DeleteBookCommand { BookId = 1 };
        var book = new Book { Id = 1, Title = "Test Book" };

        A.CallTo(() => _bookRepository.GetById(1)).Returns(book);

        //Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        //Assert
        result.Success.Should().BeTrue();
        A.CallTo(()=> _bookRepository.DeleteAsync(1)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void GivenInvalidID_ShouldFailToFindBook_ReturnFailure()
    {
        //Arrange
        var command = new DeleteBookCommand { BookId = 99 };

        A.CallTo(() => _bookRepository.GetById(99)).Returns(Task.FromResult<Book>(null));

        //Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        //Assert
        result.Success.Should().BeFalse();
        result.Error.Message.Should().Be("Could not find entity with ID 99.");
        A.CallTo(() => _bookRepository.DeleteAsync(99)).MustNotHaveHappened();
    }

    [Fact]
    public async void GivenRepositoryThrowsException_ShouldReturnFailure()
    {
        //Arrange
        var command = new DeleteBookCommand { BookId = 1 };
        var expectedExceptionMessage = "An error occurred while deleting the book.";

        A.CallTo(() => _bookRepository.GetById(1))
            .Throws(new Exception(expectedExceptionMessage));

        //Act
        var result = await _commandHandler.Handle(command, CancellationToken.None);

        //Assert
        result.Success.Should().BeFalse();
        result.Error.Message.Should().Be(expectedExceptionMessage);
        A.CallTo(() => _bookRepository.DeleteAsync(1)).MustNotHaveHappened();
    }
}