using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Webshop.Bookstore.Persistence.Context;

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

#region Database setup

var connectionString = configuration.GetConnectionString("DbConnectionString")!;
builder.Services.AddDbContext<BookstoreDbContext>(options =>
    options.UseSqlServer(connectionString));

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