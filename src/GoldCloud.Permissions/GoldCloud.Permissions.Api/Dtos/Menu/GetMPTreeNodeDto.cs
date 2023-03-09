using GoldCloud.Permissions.Api.Dtos.Common;
using System.ComponentModel.DataAnnotations;

namespace GoldCloud.Permissions.Api.Dtos.Menu
{
    /// <summary>
    /// 获取菜单数据树Dto
    /// </summary>
    public class GetMPTreeNodeDto
    {
        /// <summary>
        /// 系统标识
        /// </summary>
        public long? SystemId { get; set; }
        /// <summary>
        /// 上级标识
        /// </summary>
        public long? ParentId { get; set; } = null;

        ///// <summary>
        ///// 懒加载
        ///// </summary>
        //public bool Lazy { get; set; } = false;
    }
}
