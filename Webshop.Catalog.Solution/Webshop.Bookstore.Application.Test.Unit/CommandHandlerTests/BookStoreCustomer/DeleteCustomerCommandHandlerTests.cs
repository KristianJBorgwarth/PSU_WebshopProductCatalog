using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.DeleteCustomer;
using Webshop.Domain.Common;
using Xunit;

namespace Webshop.Bookstore.Application.Test.Unit.CommandHandlerTests.BookStoreCustomer
{
    public class DeleteCustomerCommandHandlerTests
    {
        private readonly DeleteCustomerCommandHandler _handler;
        private readonly IBookStoreCustomerRepository _fakeBookStoreCustomerRepository;

        public DeleteCustomerCommandHandlerTests()
        {
            _fakeBookStoreCustomerRepository = A.Fake<IBookStoreCustomerRepository>();
            _handler = new DeleteCustomerCommandHandler(_fakeBookStoreCustomerRepository);
        }

        [Fact]
        public async Task GivenValidCommand_ShouldReturn_ResultOk()
        {
            // Arrange
            var validId = 1;
            var cmd = new DeleteCustomerCommand { CustomerId = validId };
            var existingCustomer = new Webshop.BookStore.Domain.AggregateRoots.BookstoreCustomer { Id = validId };

            A.CallTo(() => _fakeBookStoreCustomerRepository.GetById(validId))
                .Returns(existingCustomer);
            A.CallTo(() => _fakeBookStoreCustomerRepository.DeleteAsync(validId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(cmd, CancellationToken.None);

            // Assert
            A.CallTo(() => _fakeBookStoreCustomerRepository.GetById(validId))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeBookStoreCustomerRepository.DeleteAsync(validId))
                .MustHaveHappenedOnceExactly();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async Task GivenInvalidCommand_ShouldReturn_ResultFail()
        {
            // Arrange
            var invalidId = 1;
            var cmd = new DeleteCustomerCommand { CustomerId = invalidId };

            A.CallTo(() => _fakeBookStoreCustomerRepository.GetById(invalidId))
                .Returns(Task.FromResult<Webshop.BookStore.Domain.AggregateRoots.BookstoreCustomer>(null));

            // Act
            var result = await _handler.Handle(cmd, CancellationToken.None);

            // Assert
            A.CallTo(() => _fakeBookStoreCustomerRepository.GetById(invalidId))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeBookStoreCustomerRepository.DeleteAsync(invalidId))
                .MustNotHaveHappened();
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain("Could not find entity with ID 1.");
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldReturn_ResultFail()
        {
            // Arrange
            var validId = 1;
            var cmd = new DeleteCustomerCommand { CustomerId = validId };
            var existingCustomer = new Webshop.BookStore.Domain.AggregateRoots.BookstoreCustomer { Id = validId };

            A.CallTo(() => _fakeBookStoreCustomerRepository.GetById(validId))
                .Returns(existingCustomer);

            var exceptionMessage = "Repository failed";
            A.CallTo(() => _fakeBookStoreCustomerRepository.DeleteAsync(validId))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = await _handler.Handle(cmd, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain(exceptionMessage);
            A.CallTo(() => _fakeBookStoreCustomerRepository.GetById(validId))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _fakeBookStoreCustomerRepository.DeleteAsync(validId))
                .MustHaveHappenedOnceExactly();
        }
    }
}
