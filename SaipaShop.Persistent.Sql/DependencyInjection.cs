using SaipaShop.Domain.Repositories;
using SaipaShop.Persistent.Sql.Initializing;
using SaipaShop.Persistent.Sql.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.Persistent.Sql;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistent(this IServiceCollection services, IConfiguration configuration,bool isTestEnvironment)
    {
        services.AddDbContextService(configuration,isTestEnvironment)
            .AddRepositories();
        
        
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IDatabaseInitializeService, DatabaseInitializeService>()
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddScoped<IUserRepository, UserRepository>();

        return services;

    }
}