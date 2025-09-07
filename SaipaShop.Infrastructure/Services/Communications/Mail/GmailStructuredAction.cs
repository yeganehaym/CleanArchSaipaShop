namespace SaipaShop.Infrastructure.Services.Communications.Mail;

public class GmailStructuredAction
{
    public GmailStructuredActionType Type { get; set; }
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string Language { get; set; } = "fa";
}