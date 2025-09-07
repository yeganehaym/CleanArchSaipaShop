using SaipaShop.Domain.Primitives;
using Mapster;

namespace SaipaShop.Application.Mappings.Common;

public class EmailConfig:IMapConfiguration
{
    public TypeAdapterConfig AddConfig(TypeAdapterConfig config)
    {
        config.NewConfig<Email, string>()
            .MapWith(src => src.EmailAddress);
        
        config.NewConfig<string, Email>()
            .MapWith(src => Email.Create(src));
        return config;
    }
}