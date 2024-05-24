using FakeItEasy;
using FluentAssertions;
using MediatR;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Commands.CreateBook;
using Webshop.BookStore.Application.Services.CategoryService;
using Webshop.Domain.Common;

namespace Webshop.Bookstore.Application.Test.Unit.CommandHandlerTests.Books;

public class CreateBookCommandHandlerTests
{
   private readonly IBookRepository _fakeBookRepository;
    private readonly ICategoryService _fakeCategoryService;
    private readonly CreateBookCommandHandler _createBookCommandHandler;

    public CreateBookCommandHandlerTests()
    {
        _fakeBookRepository = A.Fake<IBookRepository>();
        _fakeCategoryService = A.Fake<ICategoryService>();
        _createBookCommandHandler = new CreateBookCommandHandler(_fakeBookRepository, _fakeCategoryService);
    }

    [Fact]
    public async Task Given_ValidCommand_ShouldReturn_ResultOK()
    {
        // Arrange
        var createBookCommand = new CreateBookCommand
        {
            Title = "Test Book",
            Author = "Test Author",
            Description = "Test Description",
            Price = 10,
            CategoryId = 1,
            SellerId = 1
        };

        A.CallTo(() => _fakeCategoryService.GetCategoryAsync(createBookCommand.CategoryId))
            .Returns(Result.Ok(new CategoryResult { Name = "Test Category" }));

        // Act
        var result = await _createBookCommandHandler.Handle(createBookCommand, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        A.CallTo(() => _fakeBookRepository.CreateAsync(A<Webshop.BookStore.Domain.AggregateRoots.Book>.Ignored))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Given_InvalidCommand_ShouldReturn_ResultFail()
    {
        // Arrange
        var createBookCommand = new CreateBookCommand
        {
            Title = "Test Book",
            Author = "Test Author",
            Description = "Test Description",
            Price = 10,
            CategoryId = 1,
            SellerId = 1
        };

        A.CallTo(() => _fakeCategoryService.GetCategoryAsync(createBookCommand.CategoryId))
            .Returns(Result.Fail<CategoryResult>(Errors.General.UnspecifiedError("This shit failed")));

        // Act
        var result = await _createBookCommandHandler.Handle(createBookCommand, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        A.CallTo(() => _fakeBookRepository.CreateAsync(A<Webshop.BookStore.Domain.AggregateRoots.Book>.Ignored))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_ShouldReturn_ResultFail()
    {
        // Arrange
        var createBookCommand = new CreateBookCommand
        {
            Title = "Test Book",
            Author = "Test Author",
            Description = "Test Description",
            Price = 10,
            CategoryId = 1,
            SellerId = 1
        };

        A.CallTo(() => _fakeCategoryService.GetCategoryAsync(createBookCommand.CategoryId))
            .Returns(Result.Ok(new CategoryResult { Name = "Test Category" }));

        var exceptionMessage = "Repository failed";
        A.CallTo(() => _fakeBookRepository.CreateAsync(A<Webshop.BookStore.Domain.AggregateRoots.Book>.Ignored))
            .Throws(new Exception(exceptionMessage));

        // Act
        var result = await _createBookCommandHandler.Handle(createBookCommand, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Be(Errors.General.UnspecifiedError(exceptionMessage));
        A.CallTo(() => _fakeBookRepository.CreateAsync(A<Webshop.BookStore.Domain.AggregateRoots.Book>.Ignored))
            .MustHaveHappenedOnceExactly();
    }
}