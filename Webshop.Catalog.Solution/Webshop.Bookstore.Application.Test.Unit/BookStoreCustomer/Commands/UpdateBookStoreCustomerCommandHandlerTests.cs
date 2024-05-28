using FakeItEasy;
using FluentAssertions;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer;

namespace Webshop.Bookstore.Application.Test.Unit.BookStoreCustomer.Commands
{
    public class UpdateBookStoreCustomerCommandHandlerTests
    {
        private readonly UpdateBookStoreCustomerCommandHandler _cmdHandler;
        private readonly IBookStoreCustomerRepository _fakeBookStoreCustomerRepository;

        public UpdateBookStoreCustomerCommandHandlerTests()
        {
            _fakeBookStoreCustomerRepository = A.Fake<IBookStoreCustomerRepository>();
            _cmdHandler = new UpdateBookStoreCustomerCommandHandler(_fakeBookStoreCustomerRepository);
        }

        [Fact]
        public async Task Given_ValidCommand_ShouldReturn_ResultOk()
        {
            // Arrange
            var updateCustomerCommand = new UpdateBookStoreCustomerCommand
            {
                Id = 1,
                IsSeller = true,
                IsBuyer = false
            };

            var existingCustomer = new Webshop.BookStore.Domain.AggregateRoots.BookstoreCustomer
            {
                Id = updateCustomerCommand.Id,
                IsSeller = false,
                IsBuyer = true
            };

            A.CallTo(() => _fakeBookStoreCustomerRepository.GetById(updateCustomerCommand.Id))
                .Returns(existingCustomer);

            // Act
            var result = await _cmdHandler.Handle(updateCustomerCommand, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            existingCustomer.IsSeller.Should().BeTrue();
            existingCustomer.IsBuyer.Should().BeFalse();

            A.CallTo(() => _fakeBookStoreCustomerRepository.UpdateAsync(existingCustomer))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Given_InvalidCommand_ShouldReturn_ResultFail()
        {
            // Arrange
            var updateCustomerCommand = new UpdateBookStoreCustomerCommand
            {
                Id = 1,
                IsSeller = true,
                IsBuyer = false
            };

            A.CallTo(() => _fakeBookStoreCustomerRepository.GetById(updateCustomerCommand.Id))
                .Returns(Task.FromResult<Webshop.BookStore.Domain.AggregateRoots.BookstoreCustomer>(null));

            // Act
            var result = await _cmdHandler.Handle(updateCustomerCommand, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain("Could not find entity with ID 1.");

            A.CallTo(() => _fakeBookStoreCustomerRepository.UpdateAsync(A<Webshop.BookStore.Domain.AggregateRoots.BookstoreCustomer>.Ignored))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldReturn_ResultFail()
        {
            // Arrange
            var updateCustomerCommand = new UpdateBookStoreCustomerCommand
            {
                Id = 1,
                IsSeller = true,
                IsBuyer = false
            };

            var existingCustomer = new Webshop.BookStore.Domain.AggregateRoots.BookstoreCustomer
            {
                Id = updateCustomerCommand.Id,
                IsSeller = false,
                IsBuyer = true
            };

            A.CallTo(() => _fakeBookStoreCustomerRepository.GetById(updateCustomerCommand.Id))
                .Returns(existingCustomer);

            var exceptionMessage = "Repository failed";
            A.CallTo(() => _fakeBookStoreCustomerRepository.UpdateAsync(existingCustomer))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = await _cmdHandler.Handle(updateCustomerCommand, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Contain(exceptionMessage);

            A.CallTo(() => _fakeBookStoreCustomerRepository.UpdateAsync(existingCustomer))
                .MustHaveHappenedOnceExactly();
        }
    }
}
