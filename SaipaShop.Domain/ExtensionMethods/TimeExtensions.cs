using DNTPersianUtils.Core;

namespace SaipaShop.Domain.ExtensionMethods;

public static class TimeExtensions
{

    public static DateOnly PersianDayToDateOnly(this PersianDay persianDay)
    {
        return persianDay.PersianDayToGregorian().ToDateOnly();
    }

    /// <summary>
    /// convert string format (HH:mm) or (HHH:mm) to timeSpan
    /// </summary>
    /// <param name="timeFormat"></param>
    /// <returns></returns>
    public static TimeSpan ConvertToTimeSpan(this string timeFormat)
    {
        if (String.IsNullOrEmpty(timeFormat) || timeFormat=="___:__" || timeFormat=="__:__")
            return new TimeSpan();

        timeFormat = timeFormat.ToEnglishNumbers();
        var splits = timeFormat.Split(new[] {":"}, StringSplitOptions.None);

        var hour = int.Parse(splits[0]);
        var minute = int.Parse(splits[1]);

        if (minute >= 60)
        {
            hour += minute / 60;
            minute %= 60;
        }
        
        return new TimeSpan(hour, minute, 0);
    }

    public static string ConvertToStringFormat(this TimeSpan timeSpan,int hours=2,bool zero=false)
    {

        if (timeSpan.TotalMinutes < 1)
        {
            if (zero)
            {
                if (hours == 2)
                {
                    return "00:00";
                }

                return "000:00";
            }

            return "";
        }

        string result;
        if(timeSpan.Hours>=100 || hours==3)
            result = $"{((int) timeSpan.TotalHours).ToString("000")}:{timeSpan.Minutes.ToString("00")}";
        else
            result = $"{((int) timeSpan.TotalHours).ToString("00")}:{timeSpan.Minutes.ToString("00")}";

        return result;
    }

    public static string ConvertToStringPersianNumber(this TimeSpan timeSpan, int hours = 2, bool zero = false)
    {
        return timeSpan.ConvertToStringFormat(hours, zero).ToPersianNumbers();
    }
    

    public static string ConvertToStringFormat(this TimeOnly timeOnly)
    {
        return timeOnly.ToString("HH:mm");
    }


    
    public static DateTime AddTime(this DateTime date, string time)
    {
        var timeSplits = time.Split(":", StringSplitOptions.None);
        var datetime = new DateTime(date.Year, date.Month, date.Day, int.Parse(timeSplits[0]), int.Parse(timeSplits[1]), 0);

        return datetime;
    }
    public static DateTime AddTime(this DateTimeOffset date, string time)
    {
        var timeSplits = time.Split(":", StringSplitOptions.None);
        var datetime = new DateTime(date.Year, date.Month, date.Day, int.Parse(timeSplits[0]), int.Parse(timeSplits[1]), 0);

        return datetime;
    }
    
    public static int GetHour(this string timeSpanString)
    {
        return int.Parse(timeSpanString.Split(':')[0]);
    }
    
    public static int GetMinute(this string timeSpanString)
    {
        return int.Parse(timeSpanString.Split(':')[1]);
    }

    public static int GetMonth(this string date)
    {
        if (string.IsNullOrEmpty(date))
        {
            return 0;
        }
        return int.Parse(date.Split('/','-')[1]);
    }
    
    public static int GetDay(this string date)
    {
        if (string.IsNullOrEmpty(date))
        {
            return 0;
        }
        return int.Parse((string)date.ToEnglishNumbers().Split('/','-')[2]);
    }

    public static int GetYear(this string date)
    {
        if (string.IsNullOrEmpty(date))
        {
            return 0;
        }

        int value = 0;
        var parseResult=int.TryParse(date.Split('/','-')[0],out value);
        if (!parseResult)
            return 0;
        return value;
    }
    
    public static string SetYear(this string date, int year)
    {
        if (!string.IsNullOrEmpty(date))
        {
            var s = date.Split('/');
            s[0] = year.ToString();
            return string.Join("/", s);
        }

        return DateTime.Now.ToShortPersianDateTimeString();
    }
    
    public static TimeOnly? ConvertToTimeOnly(this string time)
    {
        
        if (String.IsNullOrEmpty(time) || time == "___:__" || time == "__:__")
            return null;
        
        return TimeOnly.Parse(time);
    }



    public static List<(DateTime Start, DateTime End)> GetRangeTimes(this DateTime startDate, List<(TimeOnly startTime, TimeSpan duration)> items)
    {
        var output = new List<(DateTime, DateTime)>();
        
        DateTime? lastDate = null;
        foreach (var item in items)
        {
            var start = new DateTime(startDate.Year, startDate.Month, startDate.Day, item.startTime.Hour,
                item.startTime.Minute, item.startTime.Second);

            if (start < startDate)
                start = start.AddDays(1);

            var end = start.Add(item.duration);
            lastDate = end;
            
            output.Add((start,end));
        }

        return output;
    }

   /// <summary>
   /// Sat is zero(starter)
   /// </summary>
   /// <param name="persianDay"></param>
   /// <returns></returns>
    public static int GetDayNumber(this PersianDay persianDay)
    {
        var weekDay = persianDay.PersianDayToGregorian().DayOfWeek;
        switch (weekDay)
        {
            case DayOfWeek.Saturday:
                return 0;
            case DayOfWeek.Sunday:
                return 1;
            case DayOfWeek.Monday:
                return 2;
            case DayOfWeek.Tuesday:
                return 3;
            case DayOfWeek.Wednesday:
                return 4;
            case DayOfWeek.Thursday:
                return 5;
            default:
                return 6;
        }
    }
   
    
    public static int GetEndDayOfMonth(this PersianDay persianDay)
    {
        return persianDay.PersianDayToGregorian().GetPersianMonthStartAndEndDates(true).LastDayNumber;
    }
    
}