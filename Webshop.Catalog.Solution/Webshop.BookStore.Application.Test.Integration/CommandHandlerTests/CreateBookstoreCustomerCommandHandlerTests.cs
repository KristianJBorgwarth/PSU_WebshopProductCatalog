using Microsoft.VisualStudio.TestPlatform.TestHost;
using Webshop.BookStore.Application.Test.Integration.Utilities;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.CommandHandlerTests;

public class CreateBookstoreCustomerCommandHandlerTests : IntegrationTestBase
{
    public CreateBookstoreCustomerCommandHandlerTests(IntegrationTestFactory<Program, BookstoreDbContext> factory) : base(factory)
    {

    }

    [Fact]
    public async Task CreateBookstoreCustomerCommandHandler_ShouldCreateCustomer()
    {

    }
}