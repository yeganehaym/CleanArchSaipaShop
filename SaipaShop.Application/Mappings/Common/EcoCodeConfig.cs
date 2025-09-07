using SaipaShop.Domain.Primitives;
using Mapster;

namespace SaipaShop.Application.Mappings.Common;

public class EcoCodeConfig:IMapConfiguration
{
    public TypeAdapterConfig AddConfig(TypeAdapterConfig config)
    {
        config.NewConfig<EconomicCode, string>()
            .MapWith( src => src.EcoCode);
        
        config.NewConfig<string, EconomicCode>()
            .MapWith(src => EconomicCode.Create(src));

        
        return config;
    }
}