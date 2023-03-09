using GoldCloud.Infrastructure.Common.Extensions;
using GoldCloud.Infrastructure.Common.Utilities;

namespace System
{
    #region 对象的获取DisplayAttribute特性的扩展方法

    /// <summary>
    /// 对象的获取DisplayAttribute特性的扩展方法
    /// </summary>
    public static class ObjectDisplayNameExtensions
    {
        #region 获取枚举上的DisplayAttribute特性的Name属性

        /// <summary>
        /// 获取枚举上的DisplayAttribute特性的Name属性
        /// </summary>
        /// <param name="enum"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum @enum, bool inherit = false)
            => AttributeHelper.GetDisplayName(@enum, inherit);

        #endregion 获取枚举上的DisplayAttribute特性的Name属性

        #region 获取枚举上的DisplayAttribute特性的Name属性

        /// <summary>
        /// 获取枚举上的DisplayAttribute特性的Name属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="byte"></param>
        /// <returns></returns>
        public static string GetDisplayName<T>(this byte @byte) where T : Enum
         => @byte.CastTo<T>().GetDisplayName(false);

        #endregion 获取枚举上的DisplayAttribute特性的Name属性
    }

    #endregion 对象的获取DisplayAttribute特性的扩展方法
}