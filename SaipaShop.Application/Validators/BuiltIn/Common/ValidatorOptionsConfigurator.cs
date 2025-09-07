using System.ComponentModel.DataAnnotations;
using SaipaShop.Application.Common;
using FluentValidation;
using FluentValidation.Resources;

namespace SaipaShop.Application.Validators.BuiltIn.Common;

public class ValidatorOptionsConfigurator
{
    public ValidatorOptionsConfigurator (ISharedLocalizer sharedLocalizer, ILanguageManager languageManager)
    {
        
        
        ValidatorOptions.Global.LanguageManager = languageManager;
        ValidatorOptions.Global.DisplayNameResolver = (type, member, experssion) =>
        {
            if (member != null)
            {
                foreach (var customAttribute in member.GetCustomAttributes(true))
                {
                    var attr = customAttribute as DisplayAttribute;
                    if (attr == null || attr.Name==null)
                        continue;
                
                    return sharedLocalizer[attr.Name].Value;
                }
                
                return sharedLocalizer[member.Name].Value;
            }
            
            return String.Empty;
            
        };

    }

    
    
}