using KingMetal.Domains.Abstractions.Attributes;
using KingMetal.Domains.Abstractions.Event;

namespace GoldCloud.Domain.Interfaces.Events.System
{
    /// <summary>
    /// 更新系统
    /// </summary>
    [Event(Name = nameof(SystemUpdateEvent))]
    public class SystemUpdateEvent : IEvent
    {
        #region 属性

        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 系统说明
        /// </summary>
        public string Remark { get; set; }

        #endregion
    }
}
