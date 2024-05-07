using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtension
{
    public static async Task InitialDatabaseMigrationAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedInitalDataAsync(context);
    }

    private static async Task SeedInitalDataAsync(ApplicationDbContext context)
    {
        await CustomerSeedDataAsync(context);
        await ProductSeedDataAsync(context);
        await OrderSeedDataAsync(context);
    }

    public static async Task CustomerSeedDataAsync(ApplicationDbContext context)
    {
        if(!await context.Customers.AnyAsync())
        {
            await context.Customers.AddRangeAsync(InitialData.Customers);
            await context.SaveChangesAsync();
        }
    }

    public static async Task ProductSeedDataAsync(ApplicationDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(InitialData.Products);
            await context.SaveChangesAsync();
        }
    }

    public static async Task OrderSeedDataAsync(ApplicationDbContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
            await context.SaveChangesAsync();
        }
    }
}
