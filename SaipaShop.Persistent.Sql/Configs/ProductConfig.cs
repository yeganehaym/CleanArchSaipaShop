using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaipaShop.Domain.Entities;

namespace SaipaShop.Persistent.Sql.Configs;

public class ProductConfig:IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsOne(x => x.Amount, b =>
        {
            b.Property(x => x.CurrencyType).HasMaxLength(3);
        });
    }
}