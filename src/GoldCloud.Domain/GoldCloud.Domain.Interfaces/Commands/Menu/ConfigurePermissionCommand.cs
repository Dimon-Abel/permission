using GoldCloud.Infrastructure.Shared.ValueObjects;
using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;
using System.Collections.Generic;

namespace GoldCloud.Domain.Interfaces.Commands.Menu
{
    [Command(Name = nameof(ConfigurePermissionCommand))]
    public class ConfigurePermissionCommand : KingMetalCommand
    {
        /// <summary>
        /// 权限标识集合
        /// </summary>
        public List<PermissionInfo> Permissions { get; set; }
    }
}
