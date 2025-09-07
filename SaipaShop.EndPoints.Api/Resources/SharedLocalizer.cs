using SaipaShop.Application.Common;
using Microsoft.Extensions.Localization;

namespace SaipaShop.EndPoints.Api.Resources;

public class SharedLocalizer : ISharedLocalizer
{
    private readonly IStringLocalizer<SharedResource> _localizer;

    public SharedLocalizer(IStringLocalizer<SharedResource> localizer)
    {
        _localizer = localizer;
    }

    public LocalizedString this[string name] => _localizer[name];

    public LocalizedString this[string name, params object[] arguments] => _localizer[name, arguments];

}