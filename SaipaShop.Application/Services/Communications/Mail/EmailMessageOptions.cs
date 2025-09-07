namespace SaipaShop.Application.Services.Communications.Mail;

public class EmailMessageOptions
{
    public List<string> To { get; set; } = new();
    public List<string>? Cc { get; set; }
    public List<string>? Bcc { get; set; }

    public string Subject { get; set; } = default!;
    public string TemplateName { get; set; } = default!; // e.g. WelcomeTemplate.html
    public Dictionary<string, object> TemplateData { get; set; } = new();

    public List<EmailAttachment>? Attachments { get; set; }

    /// <summary>
    /// آدرس ایمیلی که این ایمیل در ادامه اون ارسال میشه (برای Gmail threading)
    /// </summary>
    public string? InReplyToMessageId { get; set; }

    /// <summary>
    /// فعال‌سازی تگ‌گذاری Gmail Action یا Email Schema
    /// </summary>
    public string? StructuredAction { get; set; }

    /// <summary>
    /// فعال‌سازی ردیابی باز شدن ایمیل
    /// </summary>
    public bool EnableOpenTracking { get; set; } = false;

    public string TrackId { get; set; }
}