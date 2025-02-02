using System;

namespace AtmView.Common
{
    public static class AtmHelper
    {
        public static string Right(string str, int length)
        {
            return str.Substring(str.Length - length, length);
        }

        public static string GetHourMinuteFromDate(DateTime date)
        {
            string returnValue = Right("00" + date.Hour.ToString(), 2) + ":" + Right("00" + date.Minute.ToString(), 2);
            return returnValue;
        }

        public static bool IsDatesEqual(DateTime date1, DateTime date2)
        {
            bool returnValue = false;
            returnValue = date1.Year.ToString() == date2.Year.ToString()
                && date1.Month.ToString() == date2.Month.ToString()
                && date1.Day.ToString() == date2.Day.ToString();


            return returnValue;
        }

        public static string DiffrenceInMinutes(DateTime date1, DateTime date2)
        {
            return Math.Round((date1 - date2).TotalMinutes).ToString();
        }

        public static string DiffrenceInDaysHoursMinutes(DateTime recentdate, DateTime olddate)
        {
            TimeSpan timeSpan = recentdate - olddate;
            string timeText = string.Format("{0:D2}D{1:D2}H{2:D2}mn", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes);
            return timeText;
        }
    }
}
