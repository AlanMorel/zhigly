using System;
using System.Web;
using System.Diagnostics;

namespace Zhigly.Code
{
    public class Utility
    {

        public static void Log(string message)
        {
            Debug.WriteLine("LOGGING MESSAGE:");
            Debug.WriteLine(message);
            //Database.Log(message);
        }

        public static void Log(Exception exception)
        {

            Debug.WriteLine("LOGGING EXCEPTION: " + exception.Message);
            Debug.WriteLine(exception.StackTrace);
            //Database.Log(exception.ToString());
        }

        public static string GetIpAddress()
        {
            HttpRequest request = HttpContext.Current.Request;
            string ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
            {
                return request.ServerVariables["REMOTE_ADDR"];
            }

            string[] addresses = ip.Split(',');

            if (addresses.Length > 0)
            {
                return addresses[0];
            }

            return request.ServerVariables["REMOTE_ADDR"];
        }

        public static string GetRelativeTimeString(DateTime time)
        {
            TimeSpan timeSpan = DateTime.Now - time;
            double seconds = Math.Abs(timeSpan.TotalSeconds);

            const int second = 1;
            const int minute = 60 * second;
            const int hour = 60 * minute;
            const int day = 24 * hour;

            if (seconds < 1 * minute)
            {
                return timeSpan.Seconds == 1 ? "a second ago" : timeSpan.Seconds + " seconds ago";
            }

            if (seconds < 2 * minute)
            {
                return "a minute ago";
            }

            if (seconds < 60 * minute)
            {
                return timeSpan.Minutes + " minutes ago";
            }

            if (seconds < 120 * minute)
            {
                return "an hour ago";
            }

            if (seconds < 24 * hour)
            {
                return timeSpan.Hours + " hours ago";
            }

            if (seconds < 48 * hour)
            {
                return "yesterday";
            }

            if (seconds < 7 * day)
            {
                return timeSpan.Days + " days ago";
            }

            return "on " + time.ToString("MMMM dd").Replace(" 0", " ");
        }
    }
}