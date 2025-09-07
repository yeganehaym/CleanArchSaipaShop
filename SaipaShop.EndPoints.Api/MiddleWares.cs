using Asp.Versioning;
using Asp.Versioning.Builder;
using SaipaShop.EndPoints.Api.EndPoints.Configurations;
using SaipaShop.EndPoints.Api.Resources;
using SaipaShop.Infrastructure;
using SaipaShop.Shared.Constants;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace SaipaShop.EndPoints.Api;

public static class MiddleWares
{
    public static WebApplication UseAppSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
    public static WebApplication UseAppWares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseAppSwagger();
        }
        app.MapSwagger();

        
        app
            .UseAppCors() //cors policy
            .UseHttpsRedirection() // http to https rediction
            .UseAppLocalization() //activate localization
            .UseRouting() //activate asp.net routing
            .UseAuthentication() //activate authorizations
            .UseAuthorization()
            .UseExceptionHandler();
            
        
        app
            .UseAppSerilogConfig() // activate serilog config
            .UseAppHangFire(); //activate hangfire to set schedule jobs

        app.UseApiVersioning()
            .MapEndpoints();

        
        return app;
    }

    /// <summary>
    /// in service configs defined 2 modes for dev and prod, in here you can pick up the desired cors policy base on dev or prod mode
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static WebApplication UseAppCors(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseCors(CorsNames.DevelopmentCors);
        }
        else
        {
            app.UseCors(CorsNames.ProductionCors);
        }

        return app;
    }

    /// <summary>
    /// activate api versioning for endpoints (minimal api)
    /// tip: for controllers and actions you can use ApiVersion Attributes Family
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static IEndpointRouteBuilder UseApiVersioning(this IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        RouteGroupBuilder group = app
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet)
            .AddFluentValidationAutoValidation();

        return group;
    }


}