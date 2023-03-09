using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Event;

namespace GoldCloud.Domain.Interfaces.Events.Menu
{
    /// <summary>
    /// 禁用菜单
    /// </summary>
    [Event(Name = nameof(MenuDisableEvent))]
    public class MenuDisableEvent : IEvent
    {
        public bool Disable { get; set; }
    }
}
