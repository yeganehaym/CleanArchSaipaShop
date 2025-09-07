using SaipaShop.Domain.Primitives;
using Mapster;

namespace SaipaShop.Application.Mappings.Common;

public class NationalCodeConfig:IMapConfiguration
{
    public TypeAdapterConfig AddConfig(TypeAdapterConfig config)
    {
        config.NewConfig<string, NationalCode>()
            .MapWith(src => NationalCode.Create(src));
        
        config.NewConfig<NationalCode, string>()
            .MapWith(src => src.Code);
        return config;
    }
}