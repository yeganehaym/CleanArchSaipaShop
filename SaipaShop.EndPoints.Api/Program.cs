using MediatR;
using SaipaShop.Application;
using SaipaShop.EndPoints.Api;
using SaipaShop.EndPoints.Api.UserEndPoints;
using SaipaShop.Infrastructure;
using SaipaShop.Persistent.Sql;
using SaipaShop.Persistent.Sql.Initializing;

namespace SaipaShop.EndPoints.Api;

public class Program
{
    public static void Main(string[] args)
    {


            var builder = WebApplication.CreateBuilder(args);


            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
             

            // Add services to the container.

        
            builder.Services
                .AddPresentation()
                .AddApplication()
                .AddInfrastructure(builder.Configuration,builder.Environment)
                .AddPersistent(builder.Configuration,builder.Environment.IsEnvironment("Testing"))
                .AddControllers();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            ;

            var app = builder.Build();


            using (var scope=app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializeService>();
                dbInitializer.Migrate(scope);
            }

            app.UseAppWares();


         app.MapUsers();

            app.Run();

    }
}