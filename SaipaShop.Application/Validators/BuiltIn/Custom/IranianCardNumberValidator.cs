using DNTPersianUtils.Core;
using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

public class IranianCardNumberValidator<T,TProperty>:PropertyValidator<T,TProperty>
{
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value == null)
            return true;
        var sheba = value.ToString();

        if (String.IsNullOrEmpty(sheba))
            return true;

        return sheba.IsValidIranShetabNumber();
    }

    public override string Name => "IranianCardNumberValidator";
}