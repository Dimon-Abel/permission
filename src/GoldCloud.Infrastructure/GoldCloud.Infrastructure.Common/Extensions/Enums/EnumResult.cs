using System.Collections.Generic;

namespace GoldCloud.Infrastructure.Common.Extensions.Enums
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumResult<T> : List<EnumItem> where T : struct
    {
        /// <summary>
        /// ctor
        /// </summary>
        public EnumResult() : base(EnumItemFactory.GetEnumItems<T>())
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enums"></param>
        public EnumResult(params T[] enums) : base(EnumItemFactory.GetEnumItems(enums))
        {
        }
    }
}