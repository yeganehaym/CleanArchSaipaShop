using DNTPersianUtils.Core;
using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

/// <summary>
/// yyyy/MM/dd is a correct format
/// </summary>
public class DateFormatValidator<T,TProperty>:PropertyValidator<T,TProperty>
{

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value == null)
            return true;
        
        var date = value.ToString();
        if (string.IsNullOrEmpty(date))
            return true;

        return date.IsValidPersianDateTime(false);
    }

    public override string Name => "DateFormatValidator";
}