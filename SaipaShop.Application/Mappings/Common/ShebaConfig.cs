using SaipaShop.Domain.Primitives;
using Mapster;

namespace SaipaShop.Application.Mappings.Common;

public class ShebaConfig:IMapConfiguration
{
    public TypeAdapterConfig AddConfig(TypeAdapterConfig config)
    {
        config.NewConfig<string, Sheba>()
            .MapWith(src => Sheba.Create(src));
        
        config.NewConfig<Sheba, string>()
            .MapWith(src => src.ShebaNumber);
        return config;
    }
}