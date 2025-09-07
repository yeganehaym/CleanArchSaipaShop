using System.Reflection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.Application.Mappings.Common;

public static class MapConfigurationService
{
    public static IServiceCollection ScanMapConfigurations(this IServiceCollection services, Assembly assembly)
    {
        var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.IsPublic && t.GetInterfaces().Contains(typeof(IMapConfiguration)))
            .Select(t => (IMapConfiguration)Activator.CreateInstance(t))
            .ToList();
        
        
        var config = new TypeAdapterConfig();

        foreach (var type in types)
        {
            type.AddConfig(config);
        }


        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();
        
        return services;


    }
}