namespace SaipaShop.Infrastructure.Services.Communications.Mail;

public class MailOptions
{
    public string FromName { get; set; }
    public  string MailSender { get; set; }
    public  string SmtpServer { get; set; }
    public  int Port { get; set; }

    public  string Username { get; set; }
    public string Password { get; set; }
    
    public bool IsDeveloping { get; set; }
    public string TrackUrl { get; set; }
    public static string ConfigName => "Mail";
}