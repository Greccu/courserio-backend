using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace Courserio.Core.Helpers
{
    public static class TimeHelper
    {
        private const int SECOND = 1;
        private const int MINUTE = 60 * SECOND;
        private const int HOUR = 60 * MINUTE;
        private const int DAY = 24 * HOUR;
        private const int MONTH = 30 * DAY;

        public static string ToRelativeTime(this DateTime dateTime)
        {


            var ts = new TimeSpan(DateTime.UtcNow.Ticks - dateTime.Ticks);
            var delta = Math.Abs(ts.TotalSeconds);

            switch (delta)
            {
                case < 1 * MINUTE:
                    return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
                case < 2 * MINUTE:
                    return "a minute ago";
                case < 45 * MINUTE:
                    return ts.Minutes + " minutes ago";
                case < 90 * MINUTE:
                    return "an hour ago";
                case < 24 * HOUR:
                    return ts.Hours + " hours ago";
                case < 48 * HOUR:
                    return "yesterday";
                case < 30 * DAY:
                    return ts.Days + " days ago";
                case < 12 * MONTH:
                {
                    var months = Convert.ToInt32(Math.Floor((double) ts.Days / 30));
                    return months <= 1 ? "one month ago" : months + " months ago";
                }
                default:
                {
                    var years = Convert.ToInt32(Math.Floor((double) ts.Days / 365));
                    return years <= 1 ? "one year ago" : years + " years ago";
                }
            }
        }
    }
}
