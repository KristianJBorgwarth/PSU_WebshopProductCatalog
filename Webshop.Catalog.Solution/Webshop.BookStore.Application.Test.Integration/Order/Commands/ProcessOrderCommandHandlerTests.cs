using Microsoft.AspNetCore.Mvc.Testing;
using Webshop.BookStore.Application.Test.Integration.Utilities;

namespace Webshop.BookStore.Application.Test.Integration.Order.Commands;

public class ProcessOrderCommandHandlerTests : IntegrationTestBase
{
    protected ProcessOrderCommandHandlerTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }
}