using SaipaShop.Domain.Exceptions;

namespace SaipaShop.Domain.Primitives;

public class EconomicCode
{
    public string EcoCode { get; private set; }

    private  EconomicCode(string ecoCode)
    {
        EcoCode = ecoCode;
    }

    public static EconomicCode Create(string code)
    {
        if (String.IsNullOrEmpty(code))
        {
            throw new EmptyException(nameof(EcoCode));
        }

        if (!IsEcoCodeValid(code))
        {
            throw new InvalidException(nameof(EcoCode));
        }
        
        return new EconomicCode(code);
    }

    public static bool IsEcoCodeValid(string code)
    {
        return true;
    }
   
}