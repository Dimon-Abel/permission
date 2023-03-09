using GoldCloud.Infrastructure.Shared.ValueObjects;
using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Event;
using System.Collections.Generic;

namespace GoldCloud.Domain.Interfaces.Events.Permission
{
    /// <summary>
    /// 配置角色权限
    /// </summary>
    [Event(Name = nameof(RoleConfigurePermissionEvent))]
    public class RoleConfigurePermissionEvent : IEvent
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 权限标识集合
        /// </summary>
        public List<long> PermissionIds { get; set; }
    }
}
