using System.Text.RegularExpressions;
using SaipaShop.Domain.ExtensionMethods;
using FluentValidation;
using FluentValidation.Validators;

namespace SaipaShop.Application.Validators.BuiltIn.Custom;

/// <summary>
/// HH:mm is validFormat
/// </summary>
public class TimeFormatValidator<T,TProperty>:PropertyValidator<T,TProperty>
{
    private Regex regex = new Regex(@"\d{2}:\d{2}", RegexOptions.Compiled);

    public TimeFormatValidator()
    {
        
    }

    private int? maxHour;
    public TimeFormatValidator(int maxHour)
    {
        this.maxHour = maxHour;
    }
    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        if (value == null)
            return true;
        
        var time = value.ToString();
        if (string.IsNullOrEmpty(time))
            return true;

        time = time.Replace("_", "0");
        
        var isMatched= regex.IsMatch(time);

        if (!isMatched)
            return isMatched;
        
        if (maxHour.HasValue == false)
            return isMatched;

        var hour = TimeSpan.FromHours(maxHour.Value);
        var input = time.ConvertToTimeSpan();

        if (input > hour)
            return false;


        return true;
    }

    public override string Name => "TimeFormatValidator";
}