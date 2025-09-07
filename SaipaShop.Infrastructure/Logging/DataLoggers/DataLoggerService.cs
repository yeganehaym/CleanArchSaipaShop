using SaipaShop.Application.Services.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.Infrastructure.Logging.DataLoggers;

public static class DataLoggerService
{
    public static IServiceCollection AddDataLogger(this IServiceCollection services)
    {
        services.AddScoped<ILoggerService, LightDbLoggerService>();
        return services;
    }
}