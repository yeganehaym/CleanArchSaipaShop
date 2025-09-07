using System.Reflection;
using SaipaShop.Application.BusinessService;
using SaipaShop.Application.Common;
using SaipaShop.Application.CQRS.Behaviours;
using SaipaShop.Application.CQRS.Query;
using SaipaShop.Application.Dto.Users;
using SaipaShop.Application.Mappings.Common;
using SaipaShop.Application.Validators.BuiltIn.Common;
using SaipaShop.Domain.BusinessServices;
using SaipaShop.Infrastructure.MediatR.Behaviors.Caching;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        
        
        
        services
            
            .AddAuditLogBehavior()
            .AddCachingBehavior()
            .AddBusinessService()
            //.AddFluentValidationConfiguration()
            .ScanMapConfigurations(Assembly.GetExecutingAssembly())
            .AddValidatorsFromAssemblyContaining(typeof(LoginQuery))
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        return services;
    }

    public static IServiceCollection AddCachingBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        return services;
    }
    public static IServiceCollection AddAuditLogBehavior(this IServiceCollection services)
    {
        services
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(AuditBehavior<,>));
        return services;
    }
    
    public static IServiceCollection AddPerformanceCounterBehavior(this IServiceCollection services)
    {
        services
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
        return services;
    }
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services
            .AddScoped<IBusinessTestService, BusinessTestService>();

        return services;
    }
}