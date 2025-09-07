namespace SaipaShop.Infrastructure.Logging.Elmah;

public class AppElmahOptions
{
    public  string Path { get; set; }
    public string MailReceiver { get; set; }
    public static string ConfigName =>"ElmahOptions";
}