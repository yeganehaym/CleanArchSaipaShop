using DNTPersianUtils.Core;

namespace SaipaShop.Domain.ExtensionMethods;

public static class TimeUtils
{
    public static DateTime ToDateTime(string date, string time)
    {
        if(time.StartsWith("24"))
            time = "00" + time.Substring(2);

        var date1 = date.ToGregorianDateTime(false, 1400).Value;
        var timeSplits = time.Split(":", StringSplitOptions.None);
        var datetime = new DateTime(date1.Year, date1.Month, date1.Day, int.Parse(timeSplits[0]), int.Parse(timeSplits[1]), 0);

        return datetime;
    }
}