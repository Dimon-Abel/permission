using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;

namespace GoldCloud.Domain.Interfaces.Commands.Permission
{
    /// <summary>
    /// 删除权限
    /// </summary>
    [Command(Name = nameof(DeletePermissionCommand))]
    public class DeletePermissionCommand : KingMetalCommand
    {
    }
}
