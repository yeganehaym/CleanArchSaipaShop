namespace SaipaShop.Application.Services.Globalization;

public interface IDateTimeFormatterService
{
    string FormatDateTime(DateTime dateTime);
    
    string FormatDate(DateTime dateTime);
    string FormatDate(DateOnly date);
    
    string FormatTime(DateTime dateTime);
    string FormatTime(TimeOnly time);
    
}