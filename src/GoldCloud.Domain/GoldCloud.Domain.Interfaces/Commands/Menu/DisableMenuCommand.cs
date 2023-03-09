using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Command;

namespace GoldCloud.Domain.Interfaces.Commands.Menu
{
    /// <summary>
    /// 禁用菜单
    /// </summary>
    [Command(Name = nameof(DisableMenuCommand))]
    public class DisableMenuCommand : KingMetalCommand
    {
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disable { get; set; }
    }
}
