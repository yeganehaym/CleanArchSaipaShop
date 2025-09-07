using Mapster;
using SaipaShop.Domain.Primitives;

namespace SaipaShop.Application.Mappings.Common;

public class CurrencyConfig:IMapConfiguration
{
    public TypeAdapterConfig AddConfig(TypeAdapterConfig config)
    {
        config.NewConfig<decimal, Currency>()
            .MapWith(src => Currency.Create(src, "IRR"));

        config.NewConfig<Currency, decimal>()
            .MapWith(src => src.Amount);
        
        return config;
    }
}