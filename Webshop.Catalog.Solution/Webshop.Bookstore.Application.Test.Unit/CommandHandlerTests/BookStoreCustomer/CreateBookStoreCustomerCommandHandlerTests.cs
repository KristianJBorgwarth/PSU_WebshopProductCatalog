using FakeItEasy;
using FluentAssertions;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Services.CustomerService;
using Webshop.Domain.Common;
using Webshop.BookStore.Domain.AggregateRoots;
using Xunit;

namespace Webshop.Bookstore.Application.Test.Unit.CommandHandlerTests.BookStoreCustomer;

public class CreateBookStoreCustomerCommandHandlerTests
{
    private readonly CreateBookStoreCustomerCommandHandler _cmdHandler;
    private readonly ICustomerService _fakeCustomerService;
    private readonly IBookStoreCustomerRepository _fakeBookStoreCustomerRepository;

    public CreateBookStoreCustomerCommandHandlerTests()
    {
        _fakeCustomerService = A.Fake<ICustomerService>();
        _fakeBookStoreCustomerRepository = A.Fake<IBookStoreCustomerRepository>();
        _cmdHandler = new CreateBookStoreCustomerCommandHandler(_fakeCustomerService, _fakeBookStoreCustomerRepository);
    }

    [Fact]
    public async Task GivenValidCommand_ShouldReturn_ResultOk()
    {
        // Arrange
        var cmd = new CreateBookStoreCustomerCommand
        {
            CustomerId = 4,
            IsBuyer = true,
            IsSeller = false
        };

        var customerResult = Result.Ok(new CustomerResult { Name = "Eric" });

        A.CallTo(() => _fakeCustomerService.GetCustomerAsync(cmd.CustomerId))
            .Returns(customerResult);

        BookstoreCustomer capturedCustomer = null;
        A.CallTo(() => _fakeBookStoreCustomerRepository.CreateAsync(A<BookstoreCustomer>.Ignored))
            .Invokes(call => capturedCustomer = call.GetArgument<BookstoreCustomer>(0));

        // Act
        var result = await _cmdHandler.Handle(cmd, new CancellationToken());

        // Assert
        result.Success.Should().BeTrue();
        capturedCustomer.Should().NotBeNull();
        capturedCustomer.Name.Should().Be("Eric");
        capturedCustomer.BaseCustomeerId.Should().Be(cmd.CustomerId);
        capturedCustomer.IsSeller.Should().Be(cmd.IsSeller);
        capturedCustomer.IsBuyer.Should().Be(cmd.IsBuyer);

        A.CallTo(() => _fakeBookStoreCustomerRepository.CreateAsync(A<BookstoreCustomer>.That.Matches(b =>
                b.Name == "Eric" &&
                b.BaseCustomeerId == cmd.CustomerId &&
                b.IsSeller == cmd.IsSeller &&
                b.IsBuyer == cmd.IsBuyer)))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GivenInvalidCommand_ShouldReturn_ResultFail()
    {
        // Arrange
        var cmd = new CreateBookStoreCustomerCommand
        {
            CustomerId = 42069,
            IsBuyer = true,
            IsSeller = false
        };

        A.CallTo(() => _fakeCustomerService.GetCustomerAsync(cmd.CustomerId))
            .Returns(Result.Fail<CustomerResult>(Errors.General.UnspecifiedError("This shit failed")));

        // Act
        var result = await _cmdHandler.Handle(cmd, new CancellationToken());

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Message.Should().Contain("This shit failed");

        A.CallTo(() => _fakeBookStoreCustomerRepository.CreateAsync(A<BookstoreCustomer>.Ignored))
            .MustNotHaveHappened();
    }
}
