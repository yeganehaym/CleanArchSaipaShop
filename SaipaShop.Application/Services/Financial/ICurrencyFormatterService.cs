using SaipaShop.Domain.Primitives;
using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.Application.Services.Financial;

public interface ICurrencyFormatterService
{
    Result<PriceResultFormat> Format(Currency currency);
}