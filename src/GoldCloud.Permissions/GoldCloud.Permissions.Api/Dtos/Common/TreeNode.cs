using GoldCloud.Infrastructure.Shared.ValueObjects;
using System.Collections.Generic;

namespace GoldCloud.Permissions.Api.Dtos.Common
{
    /// <summary>
    /// 节点
    /// </summary>
    public class TreeNode
    {
        #region 属性

        /// <summary>
        /// 节点标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父级节点标识
        /// </summary>
        public string PId { get; set; }

        /// <summary>
        /// 是否为系统级权限
        /// </summary>
        public bool IsSystem { get; set; } = false;

        /// <summary>
        /// 节点类型    1|菜单~2|权限
        /// </summary>
        public TreeNodeType NodeType { get; set; }

        /// <summary>
        /// 节点说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<TreeNode> Children { get; set; }

        /// <summary>
        /// 权限归属系统
        /// </summary>
        public SystemInfo System { get; set; }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public TreeNode()
        {
            Children = new List<TreeNode>();
        }

        #endregion
    }

    /// <summary>
    /// 节点类型
    /// </summary>
    public enum TreeNodeType
    {
        /// <summary>
        /// 全部
        /// </summary>
        All = 0,
        /// <summary>
        /// 菜单权限
        /// </summary>
        MenuPrivilege = 1,
        /// <summary>
        /// 按钮权限
        /// </summary>
        Permission = 2,
        /// <summary>
        /// 菜单节点
        /// </summary>
        Menu = 3
    }
}
