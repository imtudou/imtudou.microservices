// <copyright file="DateTimeExtension.cs" company="Eureka">
// Copyright (c) Eureka. All rights reserved.
// </copyright>


namespace eHospital.Core.Utility.DateTimeHelper
{
    using System;

    /// <summary>
    /// 时间转字符串扩展方法
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 时间转为字符串"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime != null ? dateTime.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
        }

        /// <summary>
        /// 时间转为字符串"yyyy-MM-dd 00:00:00"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateIgnoreTimeString(this DateTime dateTime)
        {
            return dateTime != null ? dateTime.ToString("yyyy-MM-dd 00:00:00") : string.Empty;
        }

        /// <summary>
        /// 时间转为字符串"yyyy-MM-dd 00:00:00"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ToBeginDate(this DateTime dateTime)
        {
            return dateTime != null ? DateTime.Parse(dateTime.ToString("yyyy-MM-dd 00:00:00")) : default;
        }

        /// <summary>
        /// 时间转为字符串"yyyy-MM-dd 23:59:59"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ToEndDate(this DateTime dateTime)
        {
            return dateTime != null ? DateTime.Parse(dateTime.ToString("yyyy-MM-dd 23:59:59")) : default;
        }

        /// <summary>
        ///  时间转为字符串"yyyy-MM-dd"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime != null ? dateTime.ToString("yyyy-MM-dd") : string.Empty;
        }

        /// <summary>
        ///  时间转为字符串"HH:mm:ss"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToTimeString(this DateTime dateTime)
        {
            return dateTime != null ? dateTime.ToString("HH:mm:ss") : string.Empty;
        }

        /// <summary>
        ///  时间转为"20220819170923"
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToSeqNo(this DateTime dateTime)
        {
            return dateTime != null ? Convert.ToInt64(dateTime.ToString("yyyyMMddHHmmss")) : Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        /// <summary>
        /// 根据日期计算年龄(只精确到月份) 此处根据出生日期计算年龄只用于公卫系统 返回的是 1.03 表示 1年零3个月,1.11表示1年11个月
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static decimal GetAge(this DateTime dateTime)
        {
            /*
             * 根据日期计算年龄(只精确到月份) 此处根据出生日期计算年龄只用于公卫系统 返回的是 1.03 表示 1年零3个月,1.11表示1年11个月
             */
            if (dateTime == null || dateTime == DateTime.MaxValue || dateTime == DateTime.MinValue)
            {
                return 0;
            }

            decimal age = default;
            int month = (DateTime.Now.Year - dateTime.Year) * 12 + (DateTime.Now.Month - dateTime.Month);

            if (month <= 1)
            {
                age = Convert.ToDecimal("0.01");
            }
            else
            {
                var m1 = month / 12;
                var m2 = (month % 12);
                var m3 = m2 < 10 ? m2.ToString().PadLeft(2, '0') : m2.ToString();
                age = Convert.ToDecimal($"{m1}.{m3}");
            }

            return age;
        }

    }
}
