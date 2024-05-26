using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.Utilities;

public class CreateCustomerEndpointTest : IntegrationTestBase
{
    public CreateCustomerEndpointTest(IntegrationTestFactory<Program, BookstoreDbContext> factory) : base(factory)
    {

    }
}