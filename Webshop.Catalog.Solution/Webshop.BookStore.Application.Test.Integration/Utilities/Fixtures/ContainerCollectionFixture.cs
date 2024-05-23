using Microsoft.VisualStudio.TestPlatform.TestHost;
using Webshop.Bookstore.Persistence.Context;

namespace Webshop.BookStore.Application.Test.Integration.Utilities.Fixtures;

[CollectionDefinition("TestCollection")]
public class ContainerCollectionFixture : ICollectionFixture<IntegrationTestFactory<Program, BookstoreDbContext>> { }