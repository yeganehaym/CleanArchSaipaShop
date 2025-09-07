using SaipaShop.Domain.Exceptions;
using DNTPersianUtils.Core;

namespace SaipaShop.Domain.Primitives;

public class CardNumber
{
    public string CardNo { get; set; }

    private CardNumber(string cardNumber)
    {
        CardNo = cardNumber;
    }

    public static CardNumber Create(string cardNo)
    {
        if(String.IsNullOrEmpty(cardNo))
            throw new EmptyException(nameof(CardNo));

        if (!cardNo.IsValidIranShetabNumber())
            throw new InvalidException(nameof(CardNo));
        
        return new CardNumber(cardNo);
    }
    
 
}