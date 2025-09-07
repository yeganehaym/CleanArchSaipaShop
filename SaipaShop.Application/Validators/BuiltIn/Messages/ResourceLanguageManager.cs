using System.Globalization;
using SaipaShop.Application.Common;
using FluentValidation.Resources;

namespace SaipaShop.Application.Validators.BuiltIn.Messages;

public class ResourceLanguageManager : ILanguageManager
{
    private ISharedLocalizer _localizer;

    public ResourceLanguageManager(ISharedLocalizer localizer)
    {
        _localizer = localizer;
    }

    public string GetString(string key, CultureInfo culture = null)
    {
        var resource = _localizer[key];
        if (!resource.ResourceNotFound)
        {
            return resource.Value;
        }
        return key;
    }

    public bool Enabled { get; set; }
    public CultureInfo Culture { get; set; }
}