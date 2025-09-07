using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

public class DomainValidator<T,TProperty>:PropertyValidator<T,TProperty>
{
    private Regex regex =
        new Regex(
            @"(https?:\/\/)?(www\.)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)|(https?:\/\/)?(www\.)?(?!ww)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)",
            RegexOptions.Compiled);
    
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value == null)
            return true;
        
        var data = value.ToString();
        if (string.IsNullOrEmpty(data))
            return true;

        return regex.IsMatch(data);
    }

    public override string Name => "DomainValidator";
}