using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Profiles;
using Webshop.BookStore.Application.Services;
using Webshop.BookStore.Application.Services.CategoryService;
using Webshop.Bookstore.Persistence.Context;
using Webshop.BookStore.Application.Services.CustomerService;
using Webshop.Bookstore.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


#region Configuration setup

var env = builder.Environment;

var configuration = builder.Configuration;
configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile("appsettings.local.json", true, true);

if (env.IsDevelopment())
{
    configuration.AddJsonFile($"appsettings.{Environments.Development}.json", true, true);
    configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
}

#endregion

#region MediatR setup

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(CreateCustomerCommand));

});
#endregion

#region AutoMapper setup

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(RequestMappingProfile).Assembly);
    cfg.AddMaps(typeof(DtoMappingProfile).Assembly);
});
#endregion

#region ExternalServices setup

builder.Services.Configure<ExternalServiceSettings>(configuration.GetSection("ExternalServiceSettings"));

builder.Services.AddHttpClient("CustomerServiceClient", (serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ExternalServiceSettings>>().Value;
    client.BaseAddress = new Uri(settings.CustomerServiceBaseUrl);
});

builder.Services.AddHttpClient("CategoryServiceClient", (serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ExternalServiceSettings>>().Value;
    client.BaseAddress = new Uri(settings.CategoryServiceBaseUrl);
});

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
#endregion

#region Database setup

var connectionString = configuration.GetConnectionString("DbConnectionString")!;
builder.Services.AddDbContext<BookstoreDbContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

#region Repository setup
builder.Services.AddScoped<IBookStoreCustomerRepository, BookStoreCustomerRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }