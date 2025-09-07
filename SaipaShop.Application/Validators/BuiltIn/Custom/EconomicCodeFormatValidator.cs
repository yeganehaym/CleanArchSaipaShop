using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

public class EconomicCodeFormatValidator<T,TProperty>:PropertyValidator<T,TProperty>
{
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value == null)
            return true;
        
        var code = value.ToString();
        if (string.IsNullOrEmpty(code))
            return true;

        if (code.Length != 12)
            return false;
        
        // if (code.StartsWith("411"))
        //     return true;

        return true;
    }

    public override string Name => "EconomicCodeFormatValidator";
}