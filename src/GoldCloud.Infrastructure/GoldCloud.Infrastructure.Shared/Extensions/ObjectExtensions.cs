using System.Text.Json;
using System;
using System.Linq;

namespace GoldCloud.Infrastructure.Shared.Extensions
{
    /// <summary>
    /// Object 扩展
    /// </summary>
    public static class ObjectExtensions
    {

        /// <summary>
        /// 获取属性上的描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this object value)
        {
            Type type = value.GetType();
#pragma warning disable CS8604 // 可能的 null 引用参数。
            System.Reflection.MemberInfo member = type.GetMember(value.ToString()).FirstOrDefault();
#pragma warning restore CS8604 // 可能的 null 引用参数。
            return member != null ? member.GetDescription() : value.ToString();
        }


        /// <summary>
        /// 将对象类型转换为指定类型
        /// </summary>
        /// <param name="value">当前Object对象</param>
        /// <param name="conversionType">需要转换的类型</param>
        /// <returns>返回转换后的Object（目标类型）</returns>
        public static object CastTo(this object value, Type conversionType)
        {
            if (value == null)
                return null;
            if (conversionType.IsNullableType())
                conversionType = conversionType.GetUnNullableType();
            if (conversionType.IsEnum)
#pragma warning disable CS8604 // 可能的 null 引用参数。
                return Enum.Parse(conversionType, value.ToString());
#pragma warning restore CS8604 // 可能的 null 引用参数。
            if (conversionType == typeof(Guid))
#pragma warning disable CS8604 // 可能的 null 引用参数。
                return Guid.Parse(value.ToString());
#pragma warning restore CS8604 // 可能的 null 引用参数。
            return Convert.ChangeType(value, conversionType);
        }
        /// <summary>
        /// 将对象转换为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">要转换的对象</param>
        /// <returns>返回转换后的目标类型</returns>
        public static T CastTo<T>(this object value)
        {
            if (value == null || default(T) == null)
            {
                return default;
            }
            if (value.GetType() == typeof(T))
            {
                return (T)value;
            }
            object result = CastTo(value, typeof(T));
            return (T)result;
        }
        /// <summary>
        /// 将对象类型转换为指定类型，如转换失败返回指定的默认值
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="value">要转换的对象</param>
        /// <param name="defaultValue">转换失败返回指定的默认值</param>
        /// <returns></returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            try
            {
                return CastTo<T>(value);
            }
            catch (System.Exception)
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj)
        {
            if (obj is null) return default;

            return JsonSerializer.Serialize<T>(obj, new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
            });
        }
    }
}
