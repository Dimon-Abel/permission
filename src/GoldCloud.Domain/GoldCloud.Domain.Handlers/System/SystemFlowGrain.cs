using System.Threading.Tasks;
using GoldCloud.Domain.Interfaces.Events.System;
using GoldCloud.Domain.Interfaces.Handler;
using KingMetal.Domains.Abstractions.Event;
using KingMetal.Domains.Core.Grains;
using Microsoft.Extensions.Logging;

namespace GoldCloud.Domain.Handlers.System
{
    /// <summary>
    /// 系统 Flow Grain
    /// </summary>
    public class SystemFlowGrain : ObserverGrain<long>, ISystemFlowGrain
    {
        #region 增删改

        /// <summary>
        /// 创建系统
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(SystemCreateEvent @event, EventMetadata eventMetadata)
        {
            await Task.CompletedTask;
            Logger.LogInformation($"---创建系统关联---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        /// <summary>
        /// 更新系统
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(SystemUpdateEvent @event, EventMetadata eventMetadata)
        {
            await Task.CompletedTask;
            Logger.LogInformation($"---更新系统关联---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        /// <summary>
        /// 删除系统
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(SystemDeleteEvent @event, EventMetadata eventMetadata)
        {
            await Task.CompletedTask;
            Logger.LogInformation($"---删除系统关联---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        #endregion
    }
}
