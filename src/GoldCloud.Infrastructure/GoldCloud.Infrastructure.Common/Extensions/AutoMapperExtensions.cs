using AutoMapper;
using KingMetal.Infrastructures.Common.Utilities;

namespace System
{
    /// <summary>
    /// 对象扩展
    /// </summary>
    public static class AutoMapperExtensions
    {
        #region AutoMapper 实体映射

        /// <summary>
        /// AutoMapper 实体映射
        /// </summary>
        /// <typeparam name="TSource">源对象类型</typeparam>
        /// <typeparam name="TDestination">要映射的目标对象类型</typeparam>
        /// <param name="source">要映射的目标对象类</param>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, Action<IMappingOperationOptions<object, TDestination>> opts = null)
        {
            return AutoMapperHelper.GetMapper<TSource, TDestination>().Map(source, opts);
        }
        /// <summary>
        ///  AutoMapper 实体映射
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperHelper.GetMapper<TSource, TDestination>().Map<TDestination>(source);
        }
        #endregion AutoMapper 实体映射
    }
}