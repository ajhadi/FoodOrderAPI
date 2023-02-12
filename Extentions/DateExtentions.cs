namespace FoodOrderAPI.Extensions
{
    public static class DateExtensions
    {
        public static DateTime ToJakartaTime(this DateTime dt)
        {
            TimeZoneInfo jakartaTimeZone;
            try
            {
                jakartaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                jakartaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta");
            }
            return TimeZoneInfo.ConvertTimeFromUtc(dt, jakartaTimeZone);
        }
    }
}