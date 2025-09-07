using Mapster;

namespace SaipaShop.Application.Mappings.Common;

public interface IMapConfiguration
{
    TypeAdapterConfig AddConfig(TypeAdapterConfig config);
}