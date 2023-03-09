using GoldCloud.Infrastructure.Shared.ValueObjects;
using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Event;
using System.Collections.Generic;

namespace GoldCloud.Domain.Interfaces.Events.Menu
{
    /// <summary>
    /// 配置菜单权限
    /// </summary>
    [Event(Name = nameof(MenuConfigurePermissionEvent))]
    public class MenuConfigurePermissionEvent : IEvent
    {
        /// <summary>
        /// 权限标识集合
        /// </summary>
        public List<PermissionInfo> Permissions { get; set; }
    }
}
