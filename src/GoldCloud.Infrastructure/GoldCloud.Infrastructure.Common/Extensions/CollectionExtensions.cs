using System;
using System.Collections.Generic;

namespace GoldCloud.Infrastructure.Common.Extensions
{
    /// <summary>
    /// 集合扩展方法
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///  如果不存在，添加项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        /// <param name="existFunc"></param>
        public static void AddIfNotExist<T>(this ICollection<T> collection, T value, Func<T, bool> existFunc)
        {
            collection.CheckNotNull(nameof(collection));
            bool exists = existFunc == null ? collection.Contains(value) : existFunc(value);
            if (!exists)
            {
                collection.Add(value);
            }
        }
    }
}
