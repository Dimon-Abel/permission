using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;

namespace GoldCloud.Domain.Interfaces.Commands.System
{
    /// <summary>
    /// 删除系统
    /// </summary>
    [Command(Name = nameof(DeleteSystemCommand))]
    public class DeleteSystemCommand : KingMetalCommand
    {
    }
}
