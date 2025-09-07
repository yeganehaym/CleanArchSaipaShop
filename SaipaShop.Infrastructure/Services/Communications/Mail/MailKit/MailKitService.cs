using SaipaShop.Application.Services.Communications.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Scriban;

namespace SaipaShop.Infrastructure.Services.Communications.Mail.MailKit
{
    public class MailKitService:IEmailService
    {
        MailOptions _mailOptions;
        private IOptions<MailKitOptions> _mailKitOptions;
        public MailKitService(IOptions<MailOptions> mailOption,IOptions<MailKitOptions> mailKitOptions)
        {
            _mailOptions=mailOption.Value;
            _mailKitOptions = mailKitOptions;
        }

  
        
        public async Task SendMail(EmailMessageOptions emailMessageOptions)
        {
            
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_mailOptions.FromName, _mailOptions.MailSender));
            message.To.AddRange(emailMessageOptions.To.Select(MailboxAddress.Parse));
            if (emailMessageOptions.Cc?.Any() == true)
                message.Cc.AddRange(emailMessageOptions.Cc.Select(MailboxAddress.Parse));
            if (emailMessageOptions.Bcc?.Any() == true)
                message.Bcc.AddRange(emailMessageOptions.Bcc.Select(MailboxAddress.Parse));

            message.Subject = emailMessageOptions.Subject;

            if (!string.IsNullOrEmpty(emailMessageOptions.InReplyToMessageId))
            {
                message.InReplyTo = emailMessageOptions.InReplyToMessageId;
                message.References.Add(emailMessageOptions.InReplyToMessageId);
            }

            var templatePath = Path.Combine("EmailTemplates", emailMessageOptions.TemplateName);
            var templateText = await File.ReadAllTextAsync(templatePath);
            var template = Template.Parse(templateText);
            var renderedHtml = template.Render(emailMessageOptions.TemplateData, member => member.Name);

            // Gmail structured data (optional)
            if (emailMessageOptions.StructuredAction is not null)
            {
                
                renderedHtml = renderedHtml.Replace("</head>", $"{emailMessageOptions.StructuredAction}</head>");
            }

            // Open tracking
            if (emailMessageOptions.EnableOpenTracking)
            {
                var trackingPixel = $"<img src=\"{_mailOptions.TrackUrl}{emailMessageOptions.TrackId}\" width=\"1\" height=\"1\" style=\"display:none;\" />";
                renderedHtml += trackingPixel;

                
            }

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = renderedHtml
            };

            if (emailMessageOptions.Attachments?.Any() == true)
            {
                foreach (var att in emailMessageOptions.Attachments)
                {
                    bodyBuilder.Attachments.Add(att.FileName, att.Content, ContentType.Parse(att.ContentType));
                }
            }

            message.Body = bodyBuilder.ToMessageBody();

            using var smtp = MailKitProviderService.GetSmtpClient(_mailKitOptions, _mailOptions.IsDeveloping);
            await smtp.ConnectAsync(_mailOptions.SmtpServer, _mailOptions.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailOptions.Username, _mailOptions.Password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
            
        }
    }
}