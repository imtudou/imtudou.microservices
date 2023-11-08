// <copyright file="TimeStampUtility.cs" company="Eureka">
// Copyright (c) Eureka. All rights reserved.
// </copyright>

namespace eHospital.Core.Utility.DateTimeHelper
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 时间相关帮助类
    /// </summary>
    public class TimeStampUtility
    {
        /// <summary>
        /// 当前UTC时间戳，从1970年1月1日0点0 分0 秒开始到现在的秒数
        /// </summary>
        /// <returns></returns>
        public static string GetStringFromTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 当前UTC时间戳，从1970年1月1日0点0 分0 秒开始到现在的秒数
        /// </summary>
        /// <returns></returns>
        public static long GetLongFromTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return (long)ts.TotalSeconds;
        }

        /// <summary>
        /// 当前是一年的第几周
        /// </summary>
        /// <param name="curDay"></param>
        /// <returns></returns>
        public static int WeekOfYear(DateTime curDay)
        {
            int firstdayofweek = Convert.ToInt32(Convert.ToDateTime(curDay.Year.ToString() + "- " + "1-1 ").DayOfWeek);
            int days = curDay.DayOfYear;
            int daysOutOneWeek = days - (7 - firstdayofweek);
            if (daysOutOneWeek <= 0)
            {
                return 1;
            }
            else
            {
                int weeks = daysOutOneWeek / 7;
                if (daysOutOneWeek % 7 != 0)
                {
                    weeks++;
                }

                return weeks + 1;
            }
        }

        /// <summary>
        /// 获取日期所在的周日期范围
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static Tuple<DateTime, DateTime> CalcWeekDay(DateTime dateTime)
        {
            int year = dateTime.Year;
            int week = WeekOfYear(dateTime);
            var first = DateTime.MinValue;
            var last = DateTime.MinValue;
            //年份超限
            if (year < 1700 || year > 9999) return new Tuple<DateTime, DateTime>(first, last);
            //周数错误
            if (week < 1 || week > 53) return new Tuple<DateTime, DateTime>(first, last);
            //指定年范围
            DateTime start = new DateTime(year, 1, 1);
            int startWeekDay = (int)start.DayOfWeek;
            //周的起始日期
            first = start.AddDays((7 - startWeekDay) + (week - 2) * 7);
            last = first.AddDays(6);
            var tuple = new Tuple<DateTime, DateTime>(first, last);
            //结束日期跨年
            return tuple;
        }

        /// <summary>
        /// 获取间隔时间内月份数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<Tuple<int, int>> IntervalMonths(DateTime? startDate, DateTime? endDate)
        {
            var months = new List<Tuple<int, int>>();
            if (!startDate.HasValue || !endDate.HasValue)
            {
                return months;
            }

            if (startDate.Value > endDate.Value)
            {
                return months;
            }

            endDate = new DateTime(endDate.Value.Year, endDate.Value.Month, 1);
            DateTime dateTime = new DateTime(startDate.Value.Year, startDate.Value.Month, 1);
            do
            {
                months.Add(new Tuple<int, int>(dateTime.Year, dateTime.Month));
                dateTime = dateTime.AddMonths(1);
            }

            while (endDate.Value > dateTime);
            return months;
        }

        /// <summary>
        /// 获取间隔时间内周份数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<Tuple<int, int, DateTime, DateTime>> IntervalWeeks(DateTime? startDate, DateTime? endDate)
        {
            var weeks = new List<Tuple<int, int, DateTime, DateTime>>();
            if (!startDate.HasValue || !endDate.HasValue)
            {
                return weeks;
            }

            if (startDate.Value > endDate.Value)
            {
                return weeks;
            }

            var t1 = CalcWeekDay(startDate.Value);
            var t2 = CalcWeekDay(endDate.Value);
            DateTime dateTime = t1.Item1;
            do
            {
                weeks.Add(new Tuple<int, int, DateTime, DateTime>(dateTime.Year, WeekOfYear(dateTime), dateTime, dateTime.AddDays(6)));
                dateTime = dateTime.AddDays(7);
            }

            while (t2.Item1 > dateTime);
            return weeks;
        }

        /// <summary>
        /// 获取间隔时间内日数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<Tuple<int, int, int>> IntervalDays(DateTime? startDate, DateTime? endDate)
        {
            var days = new List<Tuple<int, int, int>>();
            if (!startDate.HasValue || !endDate.HasValue)
            {
                return days;
            }

            if (startDate.Value > endDate.Value)
            {
                return days;
            }

            endDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day);
            DateTime dateTime = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day);
            do
            {
                days.Add(new Tuple<int, int, int>(dateTime.Year, dateTime.Month, dateTime.Day));
                dateTime = dateTime.AddDays(1);
            }

            while (endDate.Value > dateTime);
            return days;
        }

        /// <summary>
        /// 时间循环间隔
        /// </summary>
        /// <param name="dtime">日期</param>
        /// <param name="type">1.天，2.周 3.月</param>
        /// <returns>返回</returns>
        public static string CronCreate(DateTime dtime, int type)
        {
            var cron = string.Empty;
            var times = dtime.ToString("mm HH");
            if (type == 1) // 天
            {
                cron = times + " " + "*" + " " + "*" + " " + "*";
            }
            else if (type == 2) // 周
            {
                var week = dtime.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                cron = times + " " + "*" + " " + "*" + " " + week;
            }
            else if (type == 3) // 月
            {
                var day = dtime.Day;
                cron = times + " " + day + " " + "*" + " " + "*";
            }
            else if (type == 4) // 从某日开始每日执行
            {
                // 【0 0 0 21/1 * *】  [从21日开始，每日0点]
                var day = dtime.Day;
                cron = times + " " + day + "/1" + " " + "*" + " " + "*";
            }

            if (type == 5)
            {
                times = "0/10"; // 每10分钟执行一次
                cron = times + " * * * *";
            }

            if (type == 6) // 每分钟执行一次
            {
                cron = "* * * * *";
            }

            if (type == 7)
            {
                cron = "0 0/1 * * *"; // 每1小时执行一次
            }

            return cron;
        }
    }
}
