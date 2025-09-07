using SaipaShop.Application.Services.Globalization;
using SaipaShop.Application.Services.Security;
using SaipaShop.Application.Services.Storage;
using SaipaShop.Domain.Repositories;
using SaipaShop.Infrastructure.Authentication.Jwt;
using SaipaShop.Infrastructure.Logging.DataLoggers;
using SaipaShop.Infrastructure.MediatR.Behaviors;
using SaipaShop.Infrastructure.Schedules;
using SaipaShop.Infrastructure.Services.Communications.Mail;
using SaipaShop.Infrastructure.Services.Globalization;
using SaipaShop.Infrastructure.Services.Security;
using SaipaShop.Infrastructure.Services.Storage;
using SaipaShop.Persistent.Sql.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;

namespace SaipaShop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration,IWebHostEnvironment environment)
    {
        services.AddHangfireService(configuration)
            .AddJsonWebTokenAuthentication(configuration) // activate JWT Authentication
            .AddMailServer() //add mailkit library config to send email appSettings is involved - change config as you wish
            .AddStorageService(configuration)

            //activate if you neeed it
            //.AddLoggingBehavior()
            .AddDataLogger(); //data logger is just interface and implementation to use it everywhere just as a shell and easy to replace by any system

        services.AddServices()
            .AddAppSerilogConfigs(environment);
        
        return services;
    }
    
    private static IServiceCollection AddAppSerilogConfigs(this IServiceCollection services,IWebHostEnvironment environment)
    {
        services.AddSerilog((services, LoggerConfiguration) =>
        {
            if (environment.IsDevelopment())
            {
                LoggerConfiguration
                    .MinimumLevel.Information();
            }
            else
            {
                LoggerConfiguration
                    .MinimumLevel.Warning();
            }
            LoggerConfiguration
                .ReadFrom.Services(services)
                .Enrich.WithClientIp()
                .Enrich.WithCorrelationId()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new []{new DbUpdateExceptionDestructurer()}))
                .WriteTo.Console();
        });

        return services;
    }

    private static IServiceCollection AddStorageService(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<StorageOptions>(options => configuration.GetSection("StorageOptions").Bind(options));

        services.AddSingleton<IStorageService,AWSSDKS3>();
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IHashService, HashService>();
        
        return services;
    }
}