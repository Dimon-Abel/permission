using System.Collections.Generic;

namespace GoldCloud.Library.Interceptor.Models
{
    /// <summary>
    /// 权限信息
    /// </summary>
    public class PermissionDto
    {
        #region 属性

        /// <summary>
        /// 权限标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 上级权限标识
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 是否拥有系统权限 true|有~false|无
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType Type { get; set; }

        /// <summary>
        /// 资源集合
        /// </summary>
        public List<PermissionResources> Resources { get; set; }

        #endregion
    }

    /// <summary>
    /// 权限资源
    /// </summary>
    public class PermissionResources
    {
        /// <summary>
        /// 标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 资源地址
        /// </summary>
        public string ResourcePath { get; set; }
    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionType : byte
    {
        /// <summary>
        /// 菜单
        /// </summary>
        Menu = 1,
        /// <summary>
        /// 按钮
        /// </summary>
        Button = 2
    }
}
