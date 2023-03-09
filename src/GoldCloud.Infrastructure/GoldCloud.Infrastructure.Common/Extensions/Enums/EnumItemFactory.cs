using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace GoldCloud.Infrastructure.Common.Extensions.Enums
{
    /// <summary>
    ///
    /// </summary>
    internal class EnumItemFactory
    {
        /// <summary>
        /// 获取枚举集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException">T is not enum</exception>
        public static List<EnumItem> GetEnumItems<T>() where T : struct
        {
            var type = typeof(T);

            if (!type.IsEnum)
            {
                throw new ArgumentException($"{type.FullName} is not enum.");
            }

            var values = type.GetEnumValues().Cast<T>().ToList();

            //排序
            var dict = new Dictionary<T, int>();

            foreach (var value in values)
            {
#pragma warning disable CS8604 // 可能的 null 引用参数。
                var field = type.GetField(name: value.ToString());
#pragma warning restore CS8604 // 可能的 null 引用参数。
                var attribute = field?.GetCustomAttribute<DisplayAttribute>();
                var order = attribute?.GetOrder();
                if (order.HasValue)
                {
                    dict.Add(value, order.Value);
                }
            }

            if (dict.Any())
            {
                var orderedValues = dict.OrderBy(o => o.Value).Select(o => o.Key).ToList();
                var orignals = values.Except(orderedValues).ToList();
                orderedValues.AddRange(orignals);

                values = orderedValues;
            }

            return GetEnumItems(values.ToArray());
        }

        /// <summary>
        /// 获取枚举集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enums"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">T is not enum</exception>
        public static List<EnumItem> GetEnumItems<T>(params T[] enums) where T : struct
        {
            var type = typeof(T);

            if (!type.IsEnum)
            {
                throw new ArgumentException($"{type.FullName} is not enum.");
            }

            return enums.Distinct()
                .Select(s =>
                {
                    var @enum = s as Enum;
                    var text = @enum?.GetDisplayName() ?? string.Empty;
                    var value = Convert.ToInt32(@enum).ToString();
                    return new EnumItem(text, value);
                })
                .ToList();
        }
    }
}