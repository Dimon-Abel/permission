using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;

namespace GoldCloud.Domain.Interfaces.Commands.Menu
{
    [Command(Name = nameof(MoveMenuCommand))]
    public class MoveMenuCommand : KingMetalCommand
    {
        /// <summary>
        /// 上级菜单标识
        /// </summary>
        public long? ParentId { get; set; }
    }
}
