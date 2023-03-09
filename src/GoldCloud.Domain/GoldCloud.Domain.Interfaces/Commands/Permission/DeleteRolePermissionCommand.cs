using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;

namespace GoldCloud.Domain.Interfaces.Commands.Permission
{
    /// <summary>
    /// 删除角色权限
    /// </summary>
    [Command(Name = nameof(DeleteRolePermissionCommand))]
    public class DeleteRolePermissionCommand : KingMetalCommand
    {
        /// <summary>
        /// 权限标识
        /// </summary>
        public string RoleId { get; set; }
    }
}
