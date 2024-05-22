using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Webshop.BookStore.Application.Contracts.Persistence;
using Webshop.BookStore.Application.Features.BookStoreCustomer.Commands.CreateCustomer;
using Webshop.BookStore.Application.Profiles;
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
});
#endregion

#region Services setup
builder.Services.AddScoped<ICustomerService, CustomerService>();
#endregion

#region Database setup

var connectionString = configuration.GetConnectionString("DbConnectionString")!;
builder.Services.AddDbContext<BookstoreDbContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

#region Repository setup
builder.Services.AddScoped<IBookStoreCustomerRepository, BookStoreCustomerRepository>();
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