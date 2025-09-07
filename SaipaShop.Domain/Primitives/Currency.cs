using SaipaShop.Domain.Exceptions;

namespace SaipaShop.Domain.Primitives;

public class Currency
{
    public decimal Amount { get; set; }
    public string CurrencyType { get; set; }

    private Currency(decimal amount, string currencyType)
    {
        Amount = amount;
        CurrencyType = currencyType;
    }
    
    public static Currency Create(decimal amount, string currencyType)
    {
        if (String.IsNullOrEmpty(currencyType))
        {
            throw new EmptyException(nameof(currencyType));
        }

        if (!IsValidCurrencyType(currencyType))
        {
            throw new InvalidException(nameof(currencyType));
        }
        
        return new Currency(amount, currencyType);
    }
    
    public static bool IsValidCurrencyType(string currencyType)
    {
        return true;
    }
}