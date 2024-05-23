using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Ductus.FluentDocker.Services.Impl;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace Webshop.BookStore.Application.Test.Integration.Utilities;

public class IntegrationTestFactory<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class where TDbContext : DbContext
{
    private readonly MsSqlContainer _dbContainer;
    private readonly ICompositeService _compositeService;

    public IntegrationTestFactory()
    {
        _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithCleanUp(true)
            .Build();

        _compositeService = new Builder().UseContainer()
            .UseCompose()
            .FromFile("C:\\Users\\fich2\\Desktop\\PSU_WebshopProductCatalog\\Webshop.Catalog.Solution\\docker-compose.test.yml")
            .RemoveOrphans()
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
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
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        _compositeService.Start();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        _compositeService.Dispose();
    }
}