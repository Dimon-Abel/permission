using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Event;

namespace GoldCloud.Domain.Interfaces.Events.Menu
{
    /// <summary>
    /// 删除菜单事件
    /// </summary>
    [Event(Name = nameof(MenuDeleteEvent))]
    public class MenuDeleteEvent : IEvent
    {
    }
}
