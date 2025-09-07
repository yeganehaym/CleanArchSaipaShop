using SaipaShop.Domain.ExtensionMethods;
using Mapster;

namespace SaipaShop.Application.Mappings.Common;

public class TimeSpanConfig:IMapConfiguration
{
    public TypeAdapterConfig AddConfig(TypeAdapterConfig config)
    {
        config
            .NewConfig<TimeSpan, string>()
            .MapWith(src => src.ConvertToStringFormat(2, true));
            
        config
            .NewConfig<string, TimeSpan>()
            .MapWith(src => src.ConvertToTimeSpan());

        
        config
            .NewConfig<TimeSpan, long>()
            .Map(dest => dest, src => src.Ticks);
            
        config
            .NewConfig<long, TimeSpan>()
            .Map(dest => dest, src => TimeSpan.FromTicks(src));
        
        return config;
    }
}