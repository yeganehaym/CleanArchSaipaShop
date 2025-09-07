using SaipaShop.Application.Validators.BuiltIn.Messages;
using FluentValidation.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.Application.Validators.BuiltIn.Common;

public static class FluentValidationConfiguration
{
    public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
    {
        services
            .AddSingleton<ILanguageManager, ResourceLanguageManager>()
            .AddSingleton<ValidatorOptionsConfigurator>();
        var instance=services.BuildServiceProvider().GetRequiredService<ValidatorOptionsConfigurator>();
        return services;
    }
}