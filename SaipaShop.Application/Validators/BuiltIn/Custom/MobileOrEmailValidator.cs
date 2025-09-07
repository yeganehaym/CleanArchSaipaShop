using SaipaShop.Domain.ExtensionMethods;
using DNTPersianUtils.Core;
using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

public class MobileOrEmailValidator<T,TProperty>:PropertyValidator<T,TProperty>
{
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value == null)
            return true;
        
        var data = value.ToString();
        
        if (String.IsNullOrEmpty(data))
            return true;

        var isMobile= data.IsValidIranianMobileNumber();
        var isEmail = data.IsValidEmail();

        return isMobile || isEmail;
    }

    public override string Name => "MobileOrEmailValidator";
}