using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Event;

namespace GoldCloud.Domain.Interfaces.Events.Menu
{
    /// <summary>
    /// 移动菜单
    /// </summary>
    [Event(Name = nameof(MenuMoveEvent))]
    public class MenuMoveEvent : IEvent
    {
        /// <summary>
        /// 上级菜单标识
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 菜单路径 "/" 分隔
        /// </summary>
        public string FullPath { get; set; }
    }
}
