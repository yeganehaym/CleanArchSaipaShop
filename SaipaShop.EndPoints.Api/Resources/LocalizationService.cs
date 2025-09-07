using System.Globalization;
using SaipaShop.Application.Common;
using SaipaShop.Shared.Constants;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

namespace SaipaShop.EndPoints.Api.Resources;

public static class LocalizationService
{
    public static IServiceCollection AddAppLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options =>
        {
        });

        services.AddSingleton<IStringLocalizerFactory, CaseInsensitiveResourceManagerStringLocalizerFactory>();
        services.AddSingleton<ISharedLocalizer, SharedLocalizer>();

        services.Configure<RequestLocalizationOptions>(localizationOptions =>
        {
            var supportedCultures = new List<string>();

            foreach (var language in LocalizationKeys.Languages)
            {
                supportedCultures.Add(language);
            }

            localizationOptions.SetDefaultCulture(LocalizationKeys.DefaultLanguage)
                .AddSupportedCultures(supportedCultures.ToArray())
                .AddSupportedUICultures(supportedCultures.ToArray());
            localizationOptions.ApplyCurrentCultureToResponseHeaders = true;

        });
        
        return services;
    }

    public static IApplicationBuilder UseAppLocalization(this IApplicationBuilder app)
    {
        
        app.UseRequestLocalization();

        return app;
    }
}