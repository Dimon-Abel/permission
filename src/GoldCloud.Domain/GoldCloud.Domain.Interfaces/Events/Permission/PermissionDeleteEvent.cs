using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Event;

namespace GoldCloud.Domain.Interfaces.Events.Permission
{
    /// <summary>
    /// 删除权限事件
    /// </summary>
    [Event(Name = nameof(PermissionDeleteEvent))]
    public class PermissionDeleteEvent : IEvent
    {
    }
}
