using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Event;

namespace GoldCloud.Domain.Interfaces.Events.System
{
    /// <summary>
    /// 删除系统
    /// </summary>
    [Event(Name = nameof(SystemDeleteEvent))]
    public class SystemDeleteEvent : IEvent
    {
    }
}
