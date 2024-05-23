using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.DeleteCustomer;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer;
using Webshop.Domain.Common;

namespace Webshop.Bookstore.Application.Test.Unit.CommandHandlerTests.BookStoreCustomer
{
    public class DeleteCustomerCommandHandlerTests
    {
        private readonly DeleteCustomerCommandHandler _handler;
        private readonly IBookStoreCustomerRepository fake_bookStoreCustomerRepository;

        public DeleteCustomerCommandHandlerTests()
        {
            fake_bookStoreCustomerRepository = A.Fake<IBookStoreCustomerRepository>();
            _handler = new DeleteCustomerCommandHandler(fake_bookStoreCustomerRepository);
        }

        [Fact]
        public async void GivenValidCommand_ShouldReturn_ResultOk()
        {
            //Arrange
            Guid validId = Guid.NewGuid();
            DeleteCustomerCommand cmd = new DeleteCustomerCommand()
            {
                CustomerId = validId
            };
            A.CallTo(() => fake_bookStoreCustomerRepository.DeleteCustomer(cmd.CustomerId))
                .Returns(Task.FromResult(Result.Ok()));

            //Act
            var result = await _handler.Handle(cmd, CancellationToken.None);

            // Assert
            A.CallTo(() => fake_bookStoreCustomerRepository.DeleteCustomer(cmd.CustomerId))
                .MustHaveHappenedOnceExactly();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async void GivenInvalidCommand_ShouldReturn_ResultFail()
        {
            //Arrange
            Guid invalidId = Guid.NewGuid();
            DeleteCustomerCommand cmd = new DeleteCustomerCommand()
            {
                CustomerId = invalidId
            };
            A.CallTo(() => fake_bookStoreCustomerRepository.DeleteCustomer(cmd.CustomerId))
                .Returns(Task.FromResult(Result.Fail(Errors.General.UnspecifiedError("Not working"))));

            //Act
            var result = await _handler.Handle(cmd, CancellationToken.None);

            //Assert
            A.CallTo(() => fake_bookStoreCustomerRepository.DeleteCustomer(cmd.CustomerId))
                .MustHaveHappenedOnceExactly();
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Be("Not working");

        }

    }
}
