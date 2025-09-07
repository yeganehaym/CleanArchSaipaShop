using SaipaShop.Application;
using SaipaShop.Application.Common;
using SaipaShop.Domain.Entities.Users;
using SaipaShop.Domain.Enums;
using SaipaShop.Persistent.Sql.Context;

namespace SaipaShop.UnitTests;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SaipaShop.Domain;
using SaipaShop.Persistent.Sql;

public static class TestServiceProviderFactory
{
    public static ServiceProvider Create(string dbName)
    {
        var services = new ServiceCollection();

        // 1. Add InMemory DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase(dbName);
        });

        // 2. Add actual services from your app
        services.AddScoped<IUnitOfWork, ApplicationDbContext>();
        services.AddRepositories();
        services.AddBusinessService();
        // Add other services here...

        // 3. Build provider and initialize DB
        var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        // 4. Seed optional test data
        db.Users.Add(new User() { FirstName = "ali",LastName = "yeganeh",Username = "test", Password = "test" ,Email = "test@test.com" ,MobileNo = "09123456789",Status = UserStatus.Accepted,SerialNumber = "123"});
        db.SaveChanges();

        return provider;
    }
}
