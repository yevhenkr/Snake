using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ.TimeTools
{
    public class Tools
    {
        public const string DATE_FORMAT = "yyyy.MM.dd";
        public const string TIME_FORMAT = "HH:mm:ss";
        public const string DATE_TIME_FORMAT = "yyyy.MM.dd HH:mm:ss";
        public const string DATE_TIME_FORMAT_WITHOUT_SECONDS = "yyyy.MM.dd HH:mm:00";

        // Format our new DateTime object to start at the UNIX Epoch
        public static DateTime UNIX_ZERO_DATE = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        public static string UNIX_ZERO_STRING = UNIX_ZERO_DATE.ToString(DATE_TIME_FORMAT);
        public static long UNIX_ZERO_TICKS = UNIX_ZERO_DATE.Ticks;

        public static double ToUnixStamp(DateTime dt)
        {
            return new TimeSpan(dt.Ticks - UNIX_ZERO_TICKS).TotalSeconds;
        }

        public static DateTime ToDateTime(double stamp)
        {
            return new DateTime(UNIX_ZERO_TICKS).AddSeconds(stamp);
        }

        public static DateTime ToDateTime(string dateString)
        {
            // if (string.IsNullOrEmpty(dateString))
            // {
            //     return UNIX_ZERO_DATE;
            // }
            // else
            // {
            dateString = dateString.Replace('_', ' ');
            dateString = dateString.Replace('-', '/');
            dateString = dateString.Replace('.', '/');
            return Convert.ToDateTime(dateString);
            // }
        }
    }
}