using SaipaShop.Application.Dto.Users;
using SaipaShop.Application.Mappings.Common;
using SaipaShop.Application.Validators.BuiltIn.Common;
using SaipaShop.Domain.Entities.Users;
using Mapster;

namespace SaipaShop.Application.Mappings.Users;

public class UserConfig:IMapConfiguration
{
    public TypeAdapterConfig AddConfig(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>();
        config.NewConfig<UserDto, User>();
        
        return config;
    }
}