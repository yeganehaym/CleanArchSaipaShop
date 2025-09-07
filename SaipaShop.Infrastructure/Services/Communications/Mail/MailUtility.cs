namespace SaipaShop.Infrastructure.Services.Communications.Mail;

public static class MailUtility
{
    public static bool IsValidEmail(this string email)
    {
        try
        {
            var emailAddress = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}