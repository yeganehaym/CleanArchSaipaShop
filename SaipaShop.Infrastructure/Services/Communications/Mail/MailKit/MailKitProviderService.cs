using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace SaipaShop.Infrastructure.Services.Communications.Mail.MailKit
{
    public static class MailKitProviderService
    {
       

        public static SmtpClient GetSmtpClient(IOptions<MailKitOptions> optionsSnapshot, bool isDevelopment)
        {
            if (isDevelopment)
            {
                return new DiskSmtpClient(optionsSnapshot);
            }

            return new SmtpClient();
        }
    }
}