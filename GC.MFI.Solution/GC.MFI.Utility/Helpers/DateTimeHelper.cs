using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Utility.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime FirstDateInWeek(this DateTime dt, DateTime date)
        {
            while (date.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(-1);
            return date;
        }

    }
}
