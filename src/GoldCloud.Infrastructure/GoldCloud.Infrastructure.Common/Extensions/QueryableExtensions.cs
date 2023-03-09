using System;
using System.Linq;
using System.Linq.Expressions;

namespace GoldCloud.Infrastructure.Common.Extensions
{
    /// <summary>
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="source">    </param>
        /// <param name="predicate"> </param>
        /// <param name="condition"> </param>
        /// <returns> </returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="source">    </param>
        /// <param name="pageIndex"> </param>
        /// <param name="pageSize">  </param>
        /// <returns> </returns>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            var skipCount = (pageIndex - 1) * pageSize;
            if (skipCount < 0)
            {
                skipCount = 0;
            }
            return source.Skip(skipCount).Take(pageSize);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="source">      </param>
        /// <param name="pageRequest"> </param>
        /// <returns> </returns>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> source, ValueObjects.PageRequest pageRequest)
        {
            return source.PageBy<T>(pageRequest.PageIndex, pageRequest.PageSize);
        }
    }
}