namespace SaipaShop.Application.Services.Communications.Mail;

public class EmailAttachment
{
    public string FileName { get; set; } = default!;
    public byte[] Content { get; set; } = default!;
    public string ContentType { get; set; } = "application/octet-stream";
}