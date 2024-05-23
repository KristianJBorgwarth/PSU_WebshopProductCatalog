using FakeItEasy;
using FluentAssertions;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Services.CustomerService;
using Webshop.Domain.Common;

namespace Webshop.Bookstore.Application.Test.Unit.CommandHandlerTests.BookStoreCustomer;

public class CreateCustomerCommandHandlerTests
{
    private readonly CreateCustomerCommandHandler _cmdHandler;
    private readonly ICustomerService _fakeCustomerService;
    private readonly IBookStoreCustomerRepository _fakeBookStoreCustomerRepository;

    public CreateCustomerCommandHandlerTests()
    {
        _fakeCustomerService = A.Fake<ICustomerService>();
        _fakeBookStoreCustomerRepository = A.Fake<IBookStoreCustomerRepository>();
        _cmdHandler = new CreateCustomerCommandHandler(_fakeCustomerService, _fakeBookStoreCustomerRepository);
    }

    [Fact]
    public async void GivenValidCommand_ShouldReturn_ResultOk()
    {
        // Arrange
        CreateCustomerCommand cmd = new()
        {
            CustomerId = 4,
            IsBuyer = true,
            IsSeller = false
        };

        var customerResult = Result.Ok(new CustomerResult { Name = "Eric" });

        A.CallTo(() => _fakeCustomerService.GetCustomerAsync(cmd.CustomerId))
            .Returns(customerResult);

        // Act
        var result = await _cmdHandler.Handle(cmd, new CancellationToken());

        // Assert
        A.CallTo(() => _fakeBookStoreCustomerRepository.AddCustomer(cmd.CustomerId, "Eric", cmd.IsSeller, cmd.IsBuyer))
            .MustHaveHappenedOnceExactly();

        result.Success.Should().BeTrue();
    }

    [Fact]
    public async void GivenInvalidCommand_ShouldReturn_ResultFail()
    {
        //Arrange
        CreateCustomerCommand cmd = new()
        {
            CustomerId = 42069,
            IsBuyer = true,
            IsSeller = false
        };

        A.CallTo(() => _fakeCustomerService.GetCustomerAsync(cmd.CustomerId))
            .Returns(Result.Fail<CustomerResult>(Errors.General.UnspecifiedError("This shit failed")));

        //Act
        var result = await _cmdHandler.Handle(cmd, new CancellationToken());

        //Assert
        A.CallTo(() => _fakeBookStoreCustomerRepository.AddCustomer(cmd.CustomerId, "Eric", cmd.IsSeller, cmd.IsBuyer)).MustNotHaveHappened();
        result.Success.Should().BeFalse();
        result.Error.Message.Should().Contain("This shit failed");
    }
}