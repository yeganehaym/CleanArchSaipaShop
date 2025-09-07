using SaipaShop.Persistent.Sql.Context;
using DNTPersianUtils.Core;
using DNTPersianUtils.Core.IranCities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.Persistent.Sql.Initializing;


public interface IDatabaseInitializeService
{
    Task Migrate(IServiceScope scope);
    Task SeedData(IServiceScope scope);
}

public class DatabaseInitializeService:IDatabaseInitializeService
{
    public async Task Migrate(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();
    }

    public async Task SeedData(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    }


}