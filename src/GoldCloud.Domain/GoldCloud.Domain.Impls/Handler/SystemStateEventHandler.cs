using GoldCloud.Domain.Impls.State;
using GoldCloud.Domain.Interfaces.Events.System;
using KingMetal.Domains.Core.Handler;

namespace GoldCloud.Domain.Impls.Handler
{
    /// <summary>
    /// 系统状态事件处理器
    /// </summary>
    public class SystemStateEventHandler : KingMetalStateEventHandler<SystemState>
    {
        #region 增删改

        /// <summary>
        /// 创建系统
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(SystemState state, SystemCreateEvent @event)
        {
            state.Name = @event.Name;
            state.Order = @event.Order;
            state.Remark = @event.Remark;
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(SystemState state, SystemUpdateEvent @event)
        {
            state.Name = @event.Name;
            state.Order = @event.Order;
            state.Remark = @event.Remark;
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(SystemState state, SystemDeleteEvent @event) { }

        #endregion
    }
}
