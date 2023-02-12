using FoodOrderAPI.Extensions;

namespace FoodOrderAPI.Helper
{
    public class StringGenerator
    {
        public static string GenerateOrderNumber()
        {
            DateTime now = DateTime.UtcNow.ToJakartaTime();
            String r = $"ABC{now.Day}{now.Month}{now.Year}-{DateTimeOffset.Now.ToUnixTimeSeconds()}";
            return r;
        }
    }
}