using DNTPersianUtils.Core;
using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

public class CompanyLegalCode<T,TProperty>:PropertyValidator<T,TProperty>
{

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value == null)
            return true;
        
        var code = value.ToString();
        if (string.IsNullOrEmpty(code))
            return true;

        var status= code.IsValidIranianNationalLegalCode();
        return status;
    }

    public override string Name => "CompanyLegalCode";
}