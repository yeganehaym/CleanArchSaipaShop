using SaipaShop.Domain.Exceptions;
using DNTPersianUtils.Core;

namespace SaipaShop.Domain.Primitives;

public class Sheba
{


    public string ShebaNumber { get; private set; }
    private Sheba(string sheba)
    {
        ShebaNumber = sheba;
    }

    public static Sheba Create(string sheba)
    {
        if(String.IsNullOrEmpty(sheba))
            throw new EmptyException(nameof(ShebaNumber));
        
        if(!sheba.IsValidIranShebaNumber())
            throw new InvalidException(nameof(ShebaNumber));
        
        return new Sheba(sheba);
    }

}