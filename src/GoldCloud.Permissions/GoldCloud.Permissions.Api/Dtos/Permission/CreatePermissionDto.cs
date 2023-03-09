using GoldCloud.Infrastructure.Shared.Enumerations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoldCloud.Permissions.Api.Dtos
{
    /// <summary>
    /// 创建权限Dto
    /// </summary>
    public class CreatePermissionDto
    {
        /// <summary>
        /// 标识
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Required(ErrorMessage = "权限名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 指令名称
        /// </summary>
        [Required(ErrorMessage = "指令名称不能为空")]
        public string Command { get; set; }

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
        /// 权限类型    1|菜单~2|按钮
        /// </summary>
        [EnumDataType(typeof(PermissionType), ErrorMessage = "无效的权限类型")]
        [Required(ErrorMessage = "权限类型不能为空")]
        public PermissionType Type { get; set; }

        /// <summary>
        /// 所属系统
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// 资源
        /// </summary>
        public List<string> ScopeNames { get; set; }

        /// <summary>
        /// 权限备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public CreatePermissionDto() => ScopeNames = new List<string>();
    }
}
