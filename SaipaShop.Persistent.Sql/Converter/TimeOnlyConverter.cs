using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SaipaShop.Persistent.Sql.Converter;

public class TimeOnlyConverter:ValueConverter<TimeOnly, long>
{
    public TimeOnlyConverter():base(v => v.Ticks,
        convertFromProviderExpression: v => new TimeOnly(v))
    {
        
    }
}