using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.Book.Commands.UpdateBook;

namespace Webshop.Bookstore.Application.Test.Unit.Book.Commands
{
    public class UpdateBookCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly UpdateBookCommandHandler _handler;

        public UpdateBookCommandHandlerTests()
        {
            _mapper = A.Fake<IMapper>();
            _bookRepository = A.Fake<IBookRepository>();
            var logger = A.Fake<ILogger<UpdateBookCommandHandler>>();
            _handler = new(_bookRepository, _mapper, logger);
        }

        [Fact]
        public async Task Handle_IdNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var command = new UpdateBookCommand { Id = 1 };
            A.CallTo(() => _bookRepository.GetById(A<int>.That.IsEqualTo(command.Id)))
                .Returns(Task.FromResult<BookStore.Domain.AggregateRoots.Book>(null));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().BeEquivalentTo("Could not find entity with ID 1.");
            A.CallTo(() => _mapper.Map(command, A<BookStore.Domain.AggregateRoots.Book>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Handle_Success_ReturnsOkResult()
        {
            // Arrange
            var command = new UpdateBookCommand { Id = 1 };
            var book = new BookStore.Domain.AggregateRoots.Book { Id = 1 };
            A.CallTo(() => _bookRepository.GetById(A<int>.That.IsEqualTo(command.Id)))
                .Returns(Task.FromResult(book));
            A.CallTo(() => _mapper.Map(command, book)).Invokes(() => {});

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            A.CallTo(() => _bookRepository.UpdateAsync(A<BookStore.Domain.AggregateRoots.Book>.That.IsSameAs(book))).MustHaveHappenedOnceExactly();
            A.CallTo(() => _mapper.Map(command, book)).MustHaveHappenedOnceExactly();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ExceptionThrown_ReturnsFailureResult()
        {
            // Arrange
            var command = new UpdateBookCommand { Id = 1 };
            A.CallTo(() => _bookRepository.GetById(A<int>.That.IsEqualTo(command.Id)))
                .Throws<Exception>();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            A.CallTo(() => _mapper.Map(command, A<BookStore.Domain.AggregateRoots.Book>.Ignored)).MustNotHaveHappened();
        }
    }
}
