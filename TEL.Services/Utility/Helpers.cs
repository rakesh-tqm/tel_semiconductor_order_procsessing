using System.Text.RegularExpressions;

namespace TEL.Services.Utility
{
    public static class Helpers
    {
        private const string defaultTimeZone = "Japan Standard Time";
        public static DateTime? ValidDateOrNull(DateTime? date)
        {
            return date != DateTime.MinValue ? date : null;
        }

        public static DateTime ToTimeZone(this DateTime datetime, string timeZone = defaultTimeZone)
        {
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return TimeZoneInfo.ConvertTime(datetime, estZone);
        }

        public static bool IsValidEmail(string value)
        {
            return Regex.IsMatch(value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
        public static string GetUniqueFileName(this string value)
        {            
            return string.Format($"{value}{DateTime.Now.ToString("yyyyMMddHHmmss")}");

        }
    }
}
