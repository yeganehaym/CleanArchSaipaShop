using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SaipaShop.Persistent.Sql.Converter;

public class TimeSpanConverter:ValueConverter<TimeSpan, long>
{
    public TimeSpanConverter():base(v => v.Ticks,
        convertFromProviderExpression: v => new TimeSpan(v))
    {
        
    }
}