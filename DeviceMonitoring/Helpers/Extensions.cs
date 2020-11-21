using System;

namespace DeviceMonitoring.Helpers
{
    public static class Extensions
    {
        public static DateTime ArmenianDateNow(this DateTime utcDateTime)
        {
            return utcDateTime.AddHours(4);
        }
    }
}