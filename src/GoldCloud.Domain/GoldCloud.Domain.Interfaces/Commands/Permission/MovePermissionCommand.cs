using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;

namespace GoldCloud.Domain.Interfaces.Commands.Permission
{
    /// <summary>
    /// 移动权限
    /// </summary>
    [Command(Name = nameof(MovePermissionCommand))]
    public class MovePermissionCommand : KingMetalCommand
    {
        /// <summary>
        /// 上级权限标识
        /// </summary>
        public long? ParentId { get; set; }
    }
}
