using System;

namespace GoldCloud.Infrastructure.Common.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// 保留N位小数，不进行四舍五入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="n">位数</param>
        /// <returns></returns>
        public static decimal Retain(this decimal value, int n = 2)
        {
            return Math.Round(value, n, MidpointRounding.ToNegativeInfinity);
        }
    }
}