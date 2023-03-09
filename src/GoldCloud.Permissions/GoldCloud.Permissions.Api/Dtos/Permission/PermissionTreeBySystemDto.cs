using GoldCloud.Permissions.Api.Dtos.Common;
using System.Collections.Generic;

namespace GoldCloud.Permissions.Api.Dtos.Permission
{
    /// <summary>
    /// 根据所属系统获取权限树
    /// </summary>
    public class PermissionTreeBySystemDto
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
        public List<PermissionTreeNode> Children { get; set; }
    }
}
