namespace SaipaShop.Application.Services.Communications.Mail;

public class EmailStructuredAction
{
    public string Type { get; set; } = "ViewAction"; // یا ConfirmAction
    public string Url { get; set; } = default!;
    public string Name { get; set; } = default!; // متن دکمه
}