using System;
namespace API.Extension
{
    public static class DateTimeExtensions
    {
        public static int Calculator(this DateTime dtime)
        {
            var age = DateTime.Now.Year - dtime.Year;
            if (dtime.Date > DateTime.Now.AddYears(-age)) age--;
            return age;
        }
    }
}