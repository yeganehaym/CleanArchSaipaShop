namespace SaipaShop.Application.Services.Communications.Mail;

public interface IEmailService
{
    Task SendMail(EmailMessageOptions options);
}