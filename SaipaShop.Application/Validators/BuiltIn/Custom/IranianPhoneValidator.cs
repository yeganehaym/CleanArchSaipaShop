using System.Text.RegularExpressions;
using DNTPersianUtils.Core;
using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

public class IranianPhoneValidator<T,TProperty>:PropertyValidator<T,TProperty>
{
    private static readonly Regex _matchIranianPhoneNumber = new Regex(@"^0?\d?\d?[2-9][0-9]{7}$", options: RegexOptions.Compiled | RegexOptions.IgnoreCase, matchTimeout: RegexUtils.MatchTimeout);
    
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value == null)
            return true;
        var phone =value.ToString();

        if (String.IsNullOrEmpty(phone))
            return true;

        return _matchIranianPhoneNumber.IsMatch(phone);
    }

    public override string Name => "IranianPhoneValidator";
}