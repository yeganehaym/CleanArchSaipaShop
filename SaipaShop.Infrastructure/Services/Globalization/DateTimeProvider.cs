using SaipaShop.Application.Services.Globalization;

namespace SaipaShop.Infrastructure.Services.Globalization;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset GetNow()
    {
        
        
        return DateTimeOffset.Now;
    }
}