using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Event;

namespace GoldCloud.Domain.Interfaces.Events.Permission
{
    /// <summary>
    /// 删除角色权限
    /// </summary>
    [Event(Name = nameof(RolePermissionDeleteEvent))]
    public class RolePermissionDeleteEvent: IEvent
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleId { get; set; }
    }
}
