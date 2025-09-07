using SaipaShop.Application.Common;
using SaipaShop.Persistent.Sql.Context;
using SaipaShop.Persistent.Sql.Interceptors;
using SaipaShop.Shared.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.Persistent.Sql;

public static class DbContextService
{
    public static IServiceCollection AddDbContextService(this IServiceCollection services,IConfiguration configuration,bool isTestEnvironment)
    {
        var constr = configuration.GetConnectionString(ConnectionStrings.DataBaseConnectionStringName);
        services.AddEntityFrameworkSqlServer();
        
        //install and enable this package if you want geography features in mssql
        //https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer.NetTopologySuite
       // services.AddEntityFrameworkSqlServerNetTopologySuite();
       
       if (isTestEnvironment)
       {
           services.AddDbContext<ApplicationDbContext>(options =>
               options.UseInMemoryDatabase("TestDb"));
       }
       else
       {
           services.AddDbContext<ApplicationDbContext>((serviceProvider,c) => c.UseSqlServer(constr,
                   options =>
                   {
                       var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                       options.CommandTimeout(minutes);
                    
                       //enable injection services in dbContext
                       c.UseInternalServiceProvider(serviceProvider);
                    
                       //install and enable this package if you want geography features in mssql
                       //https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer.NetTopologySuite
                       //options.UseNetTopologySuite();
                   })
               .AddInterceptors(new PersianYeKeCommandInterceptor())
        
           );
       }
  

        services.AddScoped<IUnitOfWork, ApplicationDbContext>();

        return services;
    }
}