using System.ComponentModel.DataAnnotations;

namespace GoldCloud.Permissions.Api.Dtos
{
    /// <summary>
    /// 删除角色权限
    /// </summary>
    public class RemoveRolePermissionDto
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        [Required]
        public string RoleId { get; set; }
    }
}
