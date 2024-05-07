using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AudiableChangeInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEvents>();
        services.AddDbContext<ApplicationDbContext>((sp,config) =>
        {
            config.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            config.UseSqlServer(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        });
        return services;
    }
}
