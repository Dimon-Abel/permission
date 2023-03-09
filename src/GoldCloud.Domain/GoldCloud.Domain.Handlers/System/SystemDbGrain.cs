using GoldCloud.Domain.Interfaces.Events.System;
using GoldCloud.Domain.Interfaces.Handler;
using GoldCloud.Infrastructure.DataBase;
using GoldCloud.Infrastructure.DataBase.Entities;
using GoldCloud.Infrastructure.DataBase.Extensions;
using KingMetal.Domains.Abstractions.Event;
using KingMetal.Domains.Core.Grains;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GoldCloud.Domain.Handlers
{
    /// <summary>
    /// 系统 Db Grain
    /// </summary>
    public class SystemDbGrain : ObserverGrain<long>, ISystemDbGrain
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
            using var db = GetGoldPermissionDB();
            await db.InsertAsync(Mapper<SystemCreateEvent, SystemEntity>(@event, x =>
                x.AfterMap((evt, entity) =>
                    entity.Id = ActorId
                )));

            Logger.LogInformation($"---创建系统---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        /// <summary>
        /// 更新系统
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(SystemUpdateEvent @event, EventMetadata eventMetadata)
        {
            using var db = GetGoldPermissionDB();
            await db.System.Where(x => x.Id == ActorId)
                .Set(x => x.Name, @event.Name)
                .Set(x => x.Order, @event.Order)
                .Set(x => x.Remark, @event.Remark)
                .UpdateAsync();

            Logger.LogInformation($"---更新系统---DbGrain---{@event.GetDefaultName()}---事件处理, ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        /// <summary>
        /// 删除系统
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(SystemDeleteEvent @event, EventMetadata eventMetadata)
        {
            using var db = GetGoldPermissionDB();
            await db.System.Where(x => x.Id == ActorId).DeleteAsync();

            Logger.LogInformation($"---删除系统---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        #endregion

        #region private

        /// <summary>
        /// 获取数据库对象
        /// </summary>
        /// <returns> </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private GoldPermissionDB GetGoldPermissionDB() => ServiceProvider.GetGoldPermissionDB();

        #endregion 获取数据库对象

    }
}
