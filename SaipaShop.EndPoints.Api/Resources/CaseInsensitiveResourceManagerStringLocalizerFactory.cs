using System.Reflection;
using System.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace SaipaShop.EndPoints.Api.Resources;

public class CaseInsensitiveResourceManagerStringLocalizerFactory : ResourceManagerStringLocalizerFactory
{
    public CaseInsensitiveResourceManagerStringLocalizerFactory(IOptions<LocalizationOptions> localizationOptions, ILoggerFactory loggerFactory) : base(localizationOptions, loggerFactory)
    {
    }

    //unfortunately we need to use reflection to the the ResourceManager as the field is private 
    private readonly FieldInfo _field = typeof(ResourceManagerStringLocalizer).GetField("_resourceManager", BindingFlags.NonPublic | BindingFlags.Instance);

    //override this method to access the resource manager at the time it is created
    protected override ResourceManagerStringLocalizer CreateResourceManagerStringLocalizer(Assembly assembly, string baseName)
    {
        //call the base method to get the localizer, I would like to override this implementation but the fields used in the construction are private
        var localizer = base.CreateResourceManagerStringLocalizer(assembly, baseName);
        if (_field == null) return localizer;
        //set the resource manager to ignore case
        if (_field.GetValue(localizer) is ResourceManager resourceManager) resourceManager.IgnoreCase = true;
        return localizer;
    }
}