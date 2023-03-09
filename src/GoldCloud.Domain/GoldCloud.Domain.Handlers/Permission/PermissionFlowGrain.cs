using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GoldCloud.Domain.Interfaces.Commands.Permission;
using GoldCloud.Domain.Interfaces.Domain;
using GoldCloud.Domain.Interfaces.Events.Permission;
using GoldCloud.Domain.Interfaces.Handler;
using GoldCloud.Infrastructure.DataBase;
using GoldCloud.Infrastructure.DataBase.Extensions;
using KingMetal.Domains.Abstractions.Event;
using KingMetal.Domains.Core.Grains;
using LinqToDB;
using Microsoft.Extensions.Logging;

namespace GoldCloud.Domain.Handlers
{
    /// <summary>
    /// 权限 Flow Grain
    /// </summary>
    public class PermissionFlowGrain : ObserverGrain<long>, IPermissionFlowGrain
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
            await Task.CompletedTask;
            Logger.LogInformation($"---创建权限关联---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(PermissionUpdateEvent @event, EventMetadata eventMetadata)
        {
            await Task.CompletedTask;
            Logger.LogInformation($"---更新权限关联---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
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
            await db.MenuPermissionAssociations.Where(x => x.PermissionId == ActorId).DeleteAsync();
            Logger.LogInformation($"---删除权限关联---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
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
            await Task.CompletedTask;
            Logger.LogInformation($"---配置角色权限---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        #endregion

        #region 删除角色权限

        /// <summary>
        /// 删除橘色权限
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(RolePermissionDeleteEvent @event, EventMetadata eventMetadata)
        {
            await Task.CompletedTask;
            Logger.LogInformation($"---删除角色权限---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
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
            using var db = GetGoldPermissionDB();

            var query = db.Permissions.Where(x => x.ParentId == ActorId);
            foreach (var item in query)
                await GrainFactory.GetGrain<IPermissionGrain>(item.Id).Execute(new MovePermissionCommand() { ParentId = ActorId });

            Logger.LogInformation($"---移动权限---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
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
            await Task.CompletedTask;
            Logger.LogInformation($"---更新权限资源---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        #endregion

        #region private

        /// <summary>
        /// 获取数据库对象
        /// </summary>
        /// <returns> </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private GoldPermissionDB GetGoldPermissionDB() => ServiceProvider.GetGoldPermissionDB();

        #endregion
    }
}
