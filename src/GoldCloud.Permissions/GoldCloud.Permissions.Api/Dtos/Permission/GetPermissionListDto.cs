using GoldCloud.Infrastructure.Shared.Enumerations;
using GoldCloud.Infrastructure.Common.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace GoldCloud.Permissions.Api.Dtos.Permission
{
    /// <summary>
    /// 获取权限列表Dto
    /// </summary>
    public class GetPermissionListDto : PageRequest
    {
        /// <summary>
        /// 权限类型 1菜单|2按钮
        /// </summary>
        [EnumDataType(typeof(PermissionType), ErrorMessage = "无效的权限类型")]
        public PermissionType? Type { get; set; }

        /// <summary>
        /// 是否拥有系统权限 true|有~false|无
        /// </summary>
        public bool? IsSystem { get; set; }
    }
}
