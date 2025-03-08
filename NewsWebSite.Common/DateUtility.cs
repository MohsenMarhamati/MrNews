using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsWebSite.Common
{
    public static class DateUtility
    {
        public static string PersianDateTime(DateTime? Time)
        {
            if (Time == null)
            {
                return null;
            }
            string GregorianDate = Time.ToString();
            DateTime d = DateTime.Parse(GregorianDate);
            PersianCalendar pc = new PersianCalendar();
            return  (string.Format("{3}:{4} - {0}/{1}/{2}", pc.GetYear(d), pc.GetMonth(d), pc.GetDayOfMonth(d), pc.GetHour(d), pc.GetMinute(d)));
        }
    }
}
