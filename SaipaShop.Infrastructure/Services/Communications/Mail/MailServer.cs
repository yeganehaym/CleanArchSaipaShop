using SaipaShop.Application.Services.Communications.Mail;
using SaipaShop.Infrastructure.Services.Communications.Mail.MailKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SaipaShop.Infrastructure.Services.Communications.Mail;

public static class MailServer
{
    public static IServiceCollection AddMailServer(this IServiceCollection services)
    {
        
        services.AddSingleton<IEmailService,MailKitService>();
        return services;
    }
}