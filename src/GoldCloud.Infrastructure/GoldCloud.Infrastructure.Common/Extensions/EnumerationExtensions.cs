using System.Runtime.CompilerServices;

namespace System
{
    #region 枚举的相关扩展方法

    /// <summary>
    /// 枚举的相关扩展方法
    /// </summary>
    public static class EnumerationExtensions
    {
        #region 获取枚举的值

        /// <summary>
        /// 获取枚举的值
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ToByte(this Enum @enum)
            => (byte)@enum.GetHashCode();

        /// <summary>
        /// 获取枚举的值
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt32(this Enum @enum)
            => @enum.GetHashCode();

        /// <summary>
        /// 获取枚举的值
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Short(this Enum @enum)
            => (short)@enum.GetHashCode();

        #endregion
    }

    #endregion
}
