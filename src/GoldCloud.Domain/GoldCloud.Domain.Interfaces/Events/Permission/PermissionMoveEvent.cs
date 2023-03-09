using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Event;

namespace GoldCloud.Domain.Interfaces.Events.Permission
{
    /// <summary>
    /// 移动权限
    /// </summary>
    [Event(Name = nameof(PermissionMoveEvent))]
    public class PermissionMoveEvent : IEvent
    {
        /// <summary>
        /// 上级权限标识
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 权限路径 "/" 分隔
        /// </summary>
        public string Path { get; set; }
    }
}
