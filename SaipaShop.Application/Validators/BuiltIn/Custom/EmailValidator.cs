using SaipaShop.Domain.ExtensionMethods;
using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

public class EmailValidator<T,TProperty>:PropertyValidator<T,TProperty>
{
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value == null)
            return true;
        
        var email = value.ToString();
        if (string.IsNullOrEmpty(email))
            return true;

        if (String.IsNullOrEmpty(email))
            return true;

        return email.IsValidEmail();
    }

    public override string Name => "EmailValidator";
}