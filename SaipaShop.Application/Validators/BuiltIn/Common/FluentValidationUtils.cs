using SaipaShop.Application.Common;
using SaipaShop.Domain.ResultPattern;
using FluentValidation;
using SaipaShop.Domain.Errors;

namespace PharmacyPlus.Application.Validators.BuiltIn.Common;

public static class FluentValidationUtils
{
   
    public static IRuleBuilderOptions<T, TProperty> WithCustomMessage<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, string key)
    {
        var languageManager = ValidatorOptions.Global.LanguageManager;
        var message = key;
        message=languageManager.GetString(key, languageManager.Culture);

        
        rule.WithMessage(string.IsNullOrWhiteSpace(message) ? " " : message);
        return rule;
    }
    
    public static IRuleBuilderOptions<T, TProperty> WithCustomMessage<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, Error error,ISharedLocalizer localizer)
    {
        var languageManager = ValidatorOptions.Global.LanguageManager;
        
        var key = error.Message;
        var message = key;

        if (localizer != null)
        {
            try
            {
                message = localizer[key,error.Parameters].Value;

            }
            catch (Exception e)
            {
                message = key;
            }
        }

        
        rule.WithMessage(string.IsNullOrWhiteSpace(message) ? " " : message);
        return rule;
    }
    
}