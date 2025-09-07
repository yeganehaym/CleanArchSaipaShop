namespace SaipaShop.Application.Services.Globalization;

public interface IDateTimeProvider
{
    DateTimeOffset GetNow();
}