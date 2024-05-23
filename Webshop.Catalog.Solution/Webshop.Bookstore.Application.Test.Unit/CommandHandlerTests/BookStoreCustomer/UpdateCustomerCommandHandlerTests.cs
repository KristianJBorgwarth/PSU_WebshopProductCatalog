using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.UpdateCustomer;
using Webshop.Domain.Common;

namespace Webshop.Bookstore.Application.Test.Unit.CommandHandlerTests.BookStoreCustomer
{
    public class UpdateCustomerCommandHandlerTests
    {
        private readonly UpdateCustomerCommandHandler _handler;
        private readonly IBookStoreCustomerRepository fake_bookStoreCustomerRepository;

        public UpdateCustomerCommandHandlerTests()
        {
            fake_bookStoreCustomerRepository = A.Fake<IBookStoreCustomerRepository>();
            _handler = new UpdateCustomerCommandHandler(fake_bookStoreCustomerRepository);
        }
        [Fact]
        public async void GivenValidCommand_ShouldReturn_ResultOk()
        {
            //Arrange
            Guid validId = Guid.NewGuid();
            UpdateCustomerCommand cmd = new UpdateCustomerCommand()
            {
                Id = validId,
                IsBuyer = true,
                IsSeller = false
            };
            A.CallTo(() => fake_bookStoreCustomerRepository.UpdateCustomer(cmd.Id, cmd.IsSeller, cmd.IsBuyer))
                .Returns(Task.FromResult(Result.Ok()));

            //Act
               var result = await _handler.Handle(cmd, CancellationToken.None);

            // Assert
            A.CallTo(() => fake_bookStoreCustomerRepository.UpdateCustomer(cmd.Id, cmd.IsSeller, cmd.IsBuyer))
                .MustHaveHappenedOnceExactly();
            result.Success.Should().BeTrue();
        }
        [Fact]
        public async void GivenInvalidCommand_ShouldReturn_ResultFail()
        {
            //Arrange
            Guid invalidId = Guid.NewGuid();
            UpdateCustomerCommand cmd = new UpdateCustomerCommand()
            {
                Id = invalidId,
                IsBuyer = true,
                IsSeller = false
            };
            A.CallTo(() => fake_bookStoreCustomerRepository.UpdateCustomer(cmd.Id, cmd.IsSeller, cmd.IsBuyer))
                .Returns(Task.FromResult(Result.Fail(Errors.General.UnspecifiedError("Not working"))));
            //Act
            var result = await _handler.Handle(cmd, CancellationToken.None);

            //Assert
            A.CallTo(() => fake_bookStoreCustomerRepository.UpdateCustomer(cmd.Id, cmd.IsSeller, cmd.IsBuyer))
             .MustHaveHappenedOnceExactly();
            result.Success.Should().BeFalse();
            result.Error.Message.Should().Be("Not working");
        }

    }
}
