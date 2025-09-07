using SaipaShop.Domain.Primitives;
using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.Application.Services.Financial;

public interface IPriceStrategyService
{
    Result<Currency> ToCurrency(Currency currency, string currencyType);
}