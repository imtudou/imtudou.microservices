using System;
using System.ComponentModel;
using System.Reflection;

namespace Imtudou.Core.CommonEnum.Extensions
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <returns></returns>
        public static string GetDescriptionValue(this Enum @enum)
        {
            var field = @enum.GetType().GetField(@enum.ToString());
            if (field is not null)
            {
                var objs = field.GetCustomAttribute(typeof(DescriptionAttribute));
                if (objs is not null)
                {
                    var descriptionAttribute = (DescriptionAttribute)objs;
                    return descriptionAttribute.Description;
                }
                return @enum.ToString();
            }
            return default;

        }

        /// <summary>
        /// 根据enum的name获取description
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public static string GetDescriptionValue<T>(this string enumName)
        {
            Type _enumType = typeof(T);//获取对象的枚举类型
            try
            {
                if (!_enumType.IsEnum)
                {
                    return string.Empty;
                }

                FieldInfo fi = _enumType.GetField(enumName.ToString());//获取枚举字段
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);//获取字段的所有描述属性

                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }
            catch
            {
            }
            return enumName.ToString();
        }

        /// <summary>
        /// 根据enum的value获取description
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue">enum值</param>
        /// <returns>结果</returns>
        public static string GetDescriptionValue<T>(this int enumValue)
        {
            Type enumType = typeof(T);//获取对象的枚举类型
            try
            {
                if (!enumType.IsEnum)
                {
                    return string.Empty;
                }

                string enumName = Enum.GetName(enumType, enumValue);
                FieldInfo fi = enumType.GetField(enumName);//获取枚举字段
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);//获取字段的所有描述属性
                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }
            catch
            {
            }

            return enumValue.ToString();
        }

        /// <summary>
        /// 根据enum的value获取枚举对象
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue">enum值</param>
        /// <returns></returns>
        public static T ValueToEnum<T>(this int enumValue)
        {
            Type enumType = typeof(T);//获取对象的枚举类型
            try
            {
                if (!enumType.IsEnum)
                {
                    return default(T);
                }

                return (T)Enum.Parse(enumType, enumValue.ToString(), true);
            }
            catch
            {
            }

            return default(T);
        }

        /// <summary>
        /// 根据Name获取枚举对象
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="name">Name</param>
        /// <returns>枚举对象</returns>
        public static T NameToEnum<T>(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return default(T);
            }

            Type _enumType = typeof(T);//获取对象的枚举类型
            try
            {
                if (!_enumType.IsEnum)
                {
                    return default(T);
                }

                return (T)Enum.Parse(_enumType, name, true);
            }
            catch
            { }

            return default(T);
        }

        /// <summary>
        /// 根据Description获取枚举对象
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="desc">Description</param>
        /// <returns>枚举对象</returns>
        public static T DescriptionToEnum<T>(this string desc)
        {
            if (string.IsNullOrEmpty(desc))
            {
                return default(T);
            }

            Type _enumType = typeof(T);//获取对象的枚举类型
            try
            {
                if (!_enumType.IsEnum)
                {
                    return default(T);
                }

                string[] strs = Enum.GetNames(_enumType);
                foreach (string str in strs)
                {//遍历枚举类型所有值进行匹配
                    FieldInfo fi = _enumType.GetField(str);//获取枚举字段
                    DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);//获取字段的所有描述属性

                    if (attributes != null && attributes.Length > 0 && attributes[0].Description == desc)
                    {
                        return (T)Enum.Parse(_enumType, str, true);
                    }
                }
            }
            catch
            { }

            return default(T);
        }
    }
}
