using Microsoft.Extensions.Localization;

namespace SaipaShop.Application.Common;

public interface ISharedLocalizer
{
    LocalizedString this[string name] { get; }
    LocalizedString this[string name, params object[] arguments] { get; }
}