using SaipaShop.Infrastructure.Services.Communications.Mail;
using SaipaShop.Shared.Constants;
using ElmahCore.Mvc;
using ElmahCore.Mvc.Notifiers;
using ElmahCore.Sql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.Infrastructure.Logging.Elmah;

public static class ElmahService
{
    public  static void AddElmahLogger(this IServiceCollection services,IConfiguration configuration)
    {
        var elmahOptions = new AppElmahOptions();
        configuration.GetSection(AppElmahOptions.ConfigName).Bind(elmahOptions);
            
        var mailOptions = new MailOptions();
        configuration.GetSection(MailOptions.ConfigName).Bind(mailOptions);
            
      
            
        services.AddElmah<SqlErrorLog>(options =>
        {
            options.Path = elmahOptions.Path;
            // options.OnPermissionCheck = context =>
            //     context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value == UserMode.Admin.ToString();
            options.ConnectionString = configuration.GetConnectionString(ConnectionStrings.ElmahLogConnectionStringName);

            if (!string.IsNullOrEmpty(elmahOptions.MailReceiver))
            {
                var elmahEmailOptions = new EmailOptions()
                {
                    AuthUserName = mailOptions.Username,
                    AuthPassword = mailOptions.Password,
                    MailSender = mailOptions.MailSender,
                    MailRecipient = elmahOptions.MailReceiver,
                    SmtpServer = mailOptions.SmtpServer
                };
                options.Notifiers.Add(new ErrorMailNotifier("Email",elmahEmailOptions));
            }
           
            options.Filters.Add(new ElmahNotFoundErrorFilter());
            options.LogRequestBody = true;
                
          
        });

    }
    
 
    
}