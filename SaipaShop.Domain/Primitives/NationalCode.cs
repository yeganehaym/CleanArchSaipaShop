using SaipaShop.Domain.Exceptions;
using DNTPersianUtils.Core;

namespace SaipaShop.Domain.Primitives;

public class NationalCode
{
    public string Code { get; private set; }

    private NationalCode(string code)
    {
        Code = code;
    }

    public static NationalCode Create(string code)
    {
        if (String.IsNullOrEmpty(code))
            throw new EmptyException(nameof(NationalCode));
        if (code.IsValidIranianNationalCode())
            throw new InvalidException(nameof(NationalCode));


        return new NationalCode(code);
    }
    

}