using DNTPersianUtils.Core;
using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

public class NationalCodeValidator<T,TProperty>:PropertyValidator<T,TProperty>
{
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value== null)
            return true;
        
        var nc = value.ToString();
        if (string.IsNullOrEmpty(nc))
            return true;
        
        return nc.IsValidIranianNationalCode();
    }

    public override string Name => "";
}