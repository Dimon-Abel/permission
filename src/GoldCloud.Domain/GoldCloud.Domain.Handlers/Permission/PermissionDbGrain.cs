using GoldCloud.Domain.Interfaces.Events.Permission;
using GoldCloud.Domain.Interfaces.Handler;
using GoldCloud.Infrastructure.DataBase;
using GoldCloud.Infrastructure.DataBase.Entities;
using GoldCloud.Infrastructure.DataBase.Extensions;
using KingMetal.Domains.Abstractions.Event;
using KingMetal.Domains.Core.Grains;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GoldCloud.Domain.Handlers
{
    /// <summary>
    /// 权限 DB Grain
    /// </summary>
    public class PermissionDbGrain : ObserverGrain<long>, IPermissionDbGrain
    {
        #region 增删改

        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(PermissionCreateEvent @event, EventMetadata eventMetadata)
        {
            using var db = GetGoldPermissionDB();
            await db.InsertAsync(Mapper<PermissionCreateEvent, Permission>(@event, x =>
                x.AfterMap((evt, entity) =>
                {
                    entity.Id = ActorId;
                    entity.Resource = evt.Resource;
                    entity.Remark = evt.Remark ?? evt.Name;
                })));

            Logger.LogInformation($"---创建权限---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(PermissionUpdateEvent @event, EventMetadata eventMetadata)
        {
            using var db = GetGoldPermissionDB();
            await db.Permissions.Where(x => x.Id == ActorId)
                .Set(x => x.Name, @event.Name)
                .Set(x => x.Command, @event.Command)
                .Set(x => x.Order, @event.Order)
                .Set(x => x.ParentId, @event.ParentId)
                .Set(x => x.IsSystem, @event.IsSystem)
                .Set(x => x.Type, @event.Type)
                .Set(x => x.SystemId, @event.SystemId)
                .Set(x => x.Path, @event.Path)
                .Set(x => x.Resource, @event.Resource)
                .Set(x => x.Remark, @event.Remark)
                .UpdateAsync();

            Logger.LogInformation($"---更新权限---FlowGrain---{@event.GetDefaultName()}---事件处理, ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(PermissionDeleteEvent @event, EventMetadata eventMetadata)
        {
            using var db = GetGoldPermissionDB();
            await db.Permissions.Where(x => x.Id == ActorId).DeleteAsync();

            Logger.LogInformation($"---删除权限---FlowGrain---{@event.GetDefaultName()}---事件处理, ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        #endregion

        #region 配置角色权限

        /// <summary>
        /// 配置角色权限
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(RoleConfigurePermissionEvent @event, EventMetadata eventMetadata)
        {
            using var db = GetGoldPermissionDB();

            await db.RolePermissionAssociations.Where(x => x.RoleId == @event.RoleId).DeleteAsync();
            var data = @event.PermissionIds.Select(x => new RolePermissionAssociation()
            {
                RoleId = @event.RoleId,
                PermissionId = x
            }).ToList();

            await db.BulkCopyAsync(data);

            Logger.LogInformation($"---配置角色权限---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        #endregion

        #region 删除角色权限

        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(RolePermissionDeleteEvent @event, EventMetadata eventMetadata)
        {
            using var db = GetGoldPermissionDB();
            await db.RolePermissionAssociations.Where(x => x.RoleId == @event.RoleId).DeleteAsync();

            Logger.LogInformation($"---删除角色权限---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        #endregion

        #region 移动权限

        /// <summary>
        /// 移动权限
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(PermissionMoveEvent @event, EventMetadata eventMetadata)
        {
            var db = GetGoldPermissionDB();

            await db.Permissions.Where(x => x.Id == ActorId)
               .Set(x => x.ParentId, @event.ParentId)
               .Set(x => x.Path, @event.Path)
               .UpdateAsync();

            Logger.LogInformation($"---移动权限---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        #endregion

        #region 更新权限资源

        /// <summary>
        /// 更新权限资源
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(ResourceUpdateEvent @event, EventMetadata eventMetadata)
        {
            var db = GetGoldPermissionDB();

            await db.Permissions.Where(x => x.Id == ActorId)
               .Set(x => x.Resource, @event.Resource)
               .UpdateAsync();

            Logger.LogInformation($"---更新权限资源---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
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
