
using Ordering.Application.Data;
using Ordering.Application.Orders.Services;
using Ordering.Infrastructure.Data.Interceptors;
using Ordering.Infrastructure.Integrations.Logistics;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp,options) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>();

            options.AddInterceptors(interceptors);

            options.UseSqlServer(connectionString);


        });
        services.AddScoped<ILogisticsGateway, MockLogisticsGateway>();
        services.AddScoped<IApplicationDbContext,ApplicationDbContext>();

        return services;
    }
}
