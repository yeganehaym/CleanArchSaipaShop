using System.Text.Json;

namespace SaipaShop.Infrastructure.Services.Communications.Mail;

public static class GmailStructuredActionBuilder
{
    public static string BuildJsonLd(GmailStructuredActionType actionType,string name,string url,string language="fa")
    {
        return BuildJsonLd(new GmailStructuredAction()
        {
            Type = actionType,
            Name = name,
            Url = url,
            Language = language
        });
    }
    public static string BuildJsonLd(GmailStructuredAction action)
    {
        var jsonObj = new Dictionary<string, object?>
        {
            ["@context"] = "http://schema.org",
            ["@type"] = action.Type.ToString(),
            ["name"] = action.Name
        };

        if (action.Type == GmailStructuredActionType.TrackAction)
        {
            jsonObj["deliveryMethod"] = "http://schema.org/OnSitePickup"; // یا OnlineDelivery
            jsonObj["target"] = new
            {
                @type = "EntryPoint",
                urlTemplate = action.Url,
                inLanguage = action.Language
            };
        }
        else if (action.Type == GmailStructuredActionType.SaveAction)
        {
            jsonObj["target"] = new
            {
                @type = "EntryPoint",
                urlTemplate = action.Url
            };
        }
        else
        {
            jsonObj["handler"] = new
            {
                @type = "HttpActionHandler",
                url = action.Url
            };
        }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(jsonObj, options);
        return $"<script type=\"application/ld+json\">\n{json}\n</script>";
    }
}