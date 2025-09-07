using SaipaShop.Domain.ExtensionMethods;
using Mapster;

namespace SaipaShop.Application.Mappings.Common;

public class TimeOnlyConfig:IMapConfiguration
{
    public TypeAdapterConfig AddConfig(TypeAdapterConfig config)
    {
        config
            .NewConfig<TimeOnly, string>()
            .MapWith(src => src.ConvertToStringFormat());
            
        config
            .NewConfig<string, TimeOnly>()
            .MapWith(src => TimeOnly.Parse(src));

        
    
        
        return config;
    }
}