using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.Bookstore.Persistence.Repositories;
// ReSharper disable ClassNeverInstantiated.Global

namespace Webshop.BookStore.Application.Test.Integration.Utilities;

public class IntegrationTestFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class where TDbContext : DbContext
{
    private readonly MsSqlContainer _dbContainer;
    private readonly ICompositeService _compositeService;

    public IntegrationTestFactory()
    {
        var projectRoot = GetProjectRootDirectory();
        var composeFilePath = Path.Combine(projectRoot, "Webshop.Catalog.Solution", "docker-compose.test.yml");

        _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithCleanUp(true)
            .Build();

        _compositeService = new Builder().UseContainer()
            .UseCompose()
            .FromFile(composeFilePath)
            .ServiceName("test")
            .RemoveOrphans()
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Override database configuration to use test database
        builder.ConfigureServices(services =>
        {
            #region Database Setup
            var descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<TDbContext>));

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<TDbContext>(options =>
            {
                options.UseSqlServer(_dbContainer.GetConnectionString());
            });
            #endregion
        });

        // Override configuration settings to use test services
        builder.ConfigureAppConfiguration((context, config) =>
        {
            var settings = new Dictionary<string, string>
            {
                ["ExternalServiceSettings:CustomerServiceBaseUrl"] = "http://localhost:18085/api/customers/",
                ["ExternalServiceSettings:CategoryServiceBaseUrl"] = "http://localhost:18084/api/categories/",
                ["Settings:SeqLogAddress"] = "http://localhost:15341",
                ["ExternalServiceSettings:PaymentServiceBaseUrl"] = "http://localhost:18083/api/payment/"
            };
            config.AddInMemoryCollection(settings);
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _compositeService.Start();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _dbContainer.DisposeAsync();
        _compositeService.Stop();
        _compositeService.Dispose();
    }

    private static string GetProjectRootDirectory()
    {
        var directory = Directory.GetCurrentDirectory();
        while (directory != null && !Directory.Exists(Path.Combine(directory, ".git")))
        {
            directory = Directory.GetParent(directory)?.FullName;
        }
        return directory ?? throw new Exception("Could not locate project root directory.");
    }
}