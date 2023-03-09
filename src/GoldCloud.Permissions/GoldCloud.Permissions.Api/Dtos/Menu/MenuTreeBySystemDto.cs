using GoldCloud.Infrastructure.Shared.ValueObjects;
using GoldCloud.Permissions.Api.Dtos.Common;
using System.Collections.Generic;

namespace GoldCloud.Permissions.Api.Dtos.Menu
{
    /// <summary>
    /// 系统菜单树
    /// </summary>
    public class MenuTreeBySystemDto
    {
        /// <summary>
        /// 系统标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 系统说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 权限树
        /// </summary>
        public List<MenuTreeNode> Children { get; set; }
    }
}
