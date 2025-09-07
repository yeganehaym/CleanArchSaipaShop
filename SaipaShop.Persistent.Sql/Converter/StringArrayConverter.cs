using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SaipaShop.Persistent.Sql.Converter
{
    public  class StringArrayConverter:ValueConverter<string[],string>
    {
        public StringArrayConverter():base(v => string.Join(',', v),
            convertFromProviderExpression: v => v.Split(',', StringSplitOptions.None))
        {
            
        }
    }
}
