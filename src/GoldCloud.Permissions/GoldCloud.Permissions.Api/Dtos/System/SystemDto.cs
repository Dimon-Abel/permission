using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldCloud.Permissions.Api.Dtos
{
    /// <summary>
    /// 系统信息
    /// </summary>
    public class SystemDto
    {
        #region 属性

        /// <summary>
        /// 系统标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 系统说明
        /// </summary>
        public string Remark { get; set; }

        #endregion
    }
}
