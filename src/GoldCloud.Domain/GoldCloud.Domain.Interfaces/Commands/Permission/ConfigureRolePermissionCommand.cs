using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;
using System.Collections.Generic;

namespace GoldCloud.Domain.Interfaces.Commands.Permission
{
    /// <summary>
    /// 配置角色权限
    /// </summary>
    [Command(Name = nameof(ConfigureRolePermissionCommand))]
    public class ConfigureRolePermissionCommand : KingMetalCommand
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 权限标识集合
        /// </summary>
        public List<long> PermissionIds { get; set; }
    }
}
