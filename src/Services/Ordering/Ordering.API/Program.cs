using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

app.UseApiServices();

if(app.Environment.IsDevelopment())
{
    await app.InitialDatabaseMigrationAsync();
}

app.Run();
