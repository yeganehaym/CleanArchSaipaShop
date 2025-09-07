using System.Reflection;
using SaipaShop.Application.Common;
using SaipaShop.Domain.ResultPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SaipaShop.Domain.Errors;

namespace SaipaShop.EndPoints.Api.EndPoints.Configurations;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IEndpointRouteBuilder MapEndpoints(
        this IEndpointRouteBuilder app,
        RouteGroupBuilder? routeGroupBuilder = null)
    {
        IEnumerable<IEndpoint> endpoints = app.ServiceProvider.GetRequiredService<IEnumerable<IEndpoint>>();

        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
        {
            
            endpoint.MapEndpoint(builder);
        }

        return app;
    }
    
    public static IResult MatchEndPoint<T>(this Result<T> result,ISharedLocalizer sharedResources=null)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }
        
        if (result.Error.ErrorType == ErrorType.NoData)
        {
            return Results.NoContent();
        }
        
        var key = result.Error.Message;
        var value = key;

        if (sharedResources != null)
        {
            var parameters = result.Error.Parameters;
            if (parameters != null && parameters.Any())
            {
                for (var index = 0; index < parameters.Length; index++)
                {
                    var parameter = parameters[index];
                    var parameterResource = sharedResources[parameter];
                    if (!parameterResource.ResourceNotFound)
                    {
                        parameters[index] = parameterResource.Value;
                    }
                }
                
                result.Error.Parameters = parameters;
            }
            
            
            value = sharedResources[key,result.Error.Parameters].Value;
            
        }

        if (result.Error.ErrorType == ErrorType.Validation)
        {
            return Results.BadRequest(new
            {
                title=value
            });
        }

        return Results.Problem(new ProblemDetails()
        {
            Title = value,
            Status = 409
        });
    }


}