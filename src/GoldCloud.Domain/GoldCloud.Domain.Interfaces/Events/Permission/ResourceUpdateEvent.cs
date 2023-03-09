using GoldCloud.Infrastructure.Shared.ValueObjects;
using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Event;
using System.Collections.Generic;

namespace GoldCloud.Domain.Interfaces.Events.Permission
{
    /// <summary>
    /// 更新权限资源
    /// </summary>
    [Event(Name = nameof(ResourceUpdateEvent))]
    public class ResourceUpdateEvent : IEvent
    {
        /// <summary>
        /// 资源
        /// </summary>
        public List<ApiScopeEntity> Resource { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public ResourceUpdateEvent()
        {
            Resource = new List<ApiScopeEntity>();
        }
    }
}
