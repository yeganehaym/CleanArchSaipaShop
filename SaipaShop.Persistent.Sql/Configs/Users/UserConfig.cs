using SaipaShop.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SaipaShop.Persistent.Sql.Configs.Users;

public class UserConfig:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.MobileNo).HasMaxLength(11);
    }
}