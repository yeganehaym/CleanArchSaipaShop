using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SaipaShop.Persistent.Sql.Converter;

public  class IntArrayConverter:ValueConverter<int[],string>
{
    public IntArrayConverter():base( v => string.Join(',', v),
        convertFromProviderExpression: v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(x=>int.Parse((string)x))
            .ToArray())
    {
        
    }
}