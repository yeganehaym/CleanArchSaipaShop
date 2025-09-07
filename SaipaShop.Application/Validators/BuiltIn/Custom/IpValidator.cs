using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

public class IpValidator<T,TProperty>:PropertyValidator<T,TProperty>
{
    
    private Regex regex =
        new Regex(
            @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            RegexOptions.Compiled);

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value == null)
            return true;
        var ip = value.ToString();
        if (string.IsNullOrEmpty(ip))
            return true;

        var isMatched=regex.IsMatch(ip);
        return isMatched;
    }

    public override string Name => "IpValidator";
}