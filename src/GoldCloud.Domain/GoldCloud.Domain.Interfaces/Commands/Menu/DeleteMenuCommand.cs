using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;

namespace GoldCloud.Domain.Interfaces.Commands.Menu
{
    /// <summary>
    /// 删除菜单
    /// </summary>
    [Command(Name = nameof(DeleteMenuCommand))]
    public class DeleteMenuCommand : KingMetalCommand
    {
    }
}
