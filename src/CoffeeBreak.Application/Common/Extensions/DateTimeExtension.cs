namespace CoffeeBreak.Application.Common.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm");
        }
    }
}
