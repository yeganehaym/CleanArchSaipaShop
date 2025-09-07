using System.Reflection;
using Asp.Versioning;
using SaipaShop.Application.Validators.BuiltIn.Messages;
using SaipaShop.EndPoints.Api.EndPoints.Configurations;
using SaipaShop.EndPoints.Api.Middlewares;
using SaipaShop.EndPoints.Api.Resources;
using SaipaShop.EndPoints.Api.Swagger;
using SaipaShop.Shared.Constants;
using FluentValidation.Resources;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace SaipaShop.EndPoints.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {

        services
            .AddFluentValidationAutoValidation()
            .AddEndpoints(Assembly.GetExecutingAssembly())
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SchemaFilter<EnumSchemaFilter>();
            })
            .AddAppLocalization()
            .AddCorsPolicies()
            .AddApiVersioning();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddExceptionHandler<DomainExceptionHandler>()
            .AddProblemDetails();
        
        

        return services;
    }


    /// <summary>
    /// add cors policies for 2 modes of dev and prod, you can change them for customization
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddCorsPolicies(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsNames.DevelopmentCors, builder =>
            {
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .Build();
            });
        });
        
        services.AddCors(options =>
        {
            options.AddPolicy(CorsNames.ProductionCors, builder =>
            {
                //add your allowable origins to access api from browser
                builder.WithOrigins(CorsNames.Origins);
            });
        });

        return services;
    }

    /// <summary>
    /// this api versioning config for default version is 1 but you can add new version later in progress of project
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddAppApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            })
            .AddMvc() // This is needed for controllers
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
    
}