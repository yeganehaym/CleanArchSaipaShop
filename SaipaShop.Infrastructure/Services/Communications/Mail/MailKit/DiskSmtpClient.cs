using System.Net;
using System.Text;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace SaipaShop.Infrastructure.Services.Communications.Mail.MailKit
{
    /// <summary>
    /// کتابخانه ای که روی دیسک به جای ارسال ایمیل استفاده میشود مناسب برای تست
    /// </summary>
    public class DiskSmtpClient : SmtpClient
    {
        public DiskSmtpClient(IOptions<MailKitOptions> mailOptionsSnapshot)
        {
            if (mailOptionsSnapshot.Value.SpecifiedPickupDirectory)
            {
                SpecifiedPickupDirectory = true;
                PickupDirectoryLocation = mailOptionsSnapshot.Value.PickupDirectoryLocation;
            }

        }
        public bool SpecifiedPickupDirectory { get; set; }
        public string PickupDirectoryLocation { get; set; }

        public override Task<string> SendAsync(MimeMessage message, CancellationToken cancellationToken = new CancellationToken(),
            ITransferProgress progress = null)
        {
            if (!SpecifiedPickupDirectory)
                return base.SendAsync(message, cancellationToken, progress);
            return SaveToPickupDirectory(message, PickupDirectoryLocation);

        }
        

        private async Task<string> SaveToPickupDirectory(MimeMessage message, string pickupDirectory)
        {
            using (var stream = new FileStream($@"{pickupDirectory}\email-{Guid.NewGuid().ToString("N")}.eml", FileMode.CreateNew))
            {
                await message.WriteToAsync(stream);
            }

            return null;
        }

        public override Task AuthenticateAsync(
            Encoding encoding,
            ICredentials credentials,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!SpecifiedPickupDirectory)
                return base.AuthenticateAsync(encoding, credentials, cancellationToken);
            return Task.CompletedTask;
        }

        public override Task AuthenticateAsync(
            SaslMechanism mechanism,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!SpecifiedPickupDirectory)
                return base.AuthenticateAsync(mechanism, cancellationToken);
            return Task.CompletedTask;
        }
        public override Task ConnectAsync(string host, int port = 0, SecureSocketOptions options = SecureSocketOptions.Auto,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (!SpecifiedPickupDirectory)
                return base.ConnectAsync(host, port, options, cancellationToken);
            return Task.CompletedTask;
        }



        public override Task DisconnectAsync(bool quit, CancellationToken cancellationToken = new CancellationToken())
        {
            if (!SpecifiedPickupDirectory)
                return base.DisconnectAsync(quit, cancellationToken);

            return Task.CompletedTask;
        }

    
    }
}