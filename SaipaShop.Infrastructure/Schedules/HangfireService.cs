using SaipaShop.Shared.Constants;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.Infrastructure.Schedules;

public static class HangfireService
{
    public static IServiceCollection AddHangfireService(this IServiceCollection services,IConfiguration config)
    {
        var constr = config.GetConnectionString(ConnectionStrings.HangFireConnectionStringName);
        // Add Hangfire services.
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(constr, new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

        // Add the processing server as IHostedService
        services.AddHangfireServer();
        //JobStorage.Current = new SqlServerStorage(constr);

        return services;
    }
}