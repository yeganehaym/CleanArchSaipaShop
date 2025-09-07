using System.Globalization;
using FluentValidation.Resources;

namespace SaipaShop.Application.Validators.BuiltIn.Messages;

/// <summary>
/// مدیری زبان اختاصی FluentValidator که ب صورت رسمی از سیستم داخلی خودش میخواند
/// </summary>
public class CustomLanguageManager:ILanguageManager
{
    public string GetString(string key, CultureInfo culture = null)
    {
	    var ret= PersianValidators.GetTranslation(key);
        return ret;
    }

    public bool Enabled { get; set; }
    public CultureInfo Culture { get; set; }
}