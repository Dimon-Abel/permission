using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GoldCloud.Domain.Interfaces.Commands.Menu;
using GoldCloud.Domain.Interfaces.Commands.Permission;
using GoldCloud.Domain.Interfaces.Domain;
using GoldCloud.Infrastructure.Shared.Enumerations;
using GoldCloud.Domain.Interfaces.Events.Menu;
using GoldCloud.Domain.Interfaces.Handler;
using GoldCloud.Infrastructure.DataBase;
using GoldCloud.Infrastructure.DataBase.Entities;
using GoldCloud.Infrastructure.DataBase.Extensions;
using KingMetal.Domains.Abstractions.Event;
using KingMetal.Domains.Core.Grains;
using LinqToDB;
using Microsoft.Extensions.Logging;

namespace GoldCloud.Domain.Handlers
{
    /// <summary>
    /// 菜单 Flow Grain
    /// </summary>
    public class MenuFlowGrain : ObserverGrain<long>, IMenuFlowGrain
    {
        #region 增删改

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(MenuCreateEvent @event, EventMetadata eventMetadata)
        {
            #region 创建系统默认菜单权限

            var grain = GrainFactory.GetGrain<IPermissionGrain>(ActorId);
            await grain.Execute(new CreatePermissionCommand()
            {
                Name = @event.Name,
                Command = @event.Name,
                Order = 10,
                ParentId = @event.ParentId,
                Type = PermissionType.Menu,
                SystemId = @event.SystemId,
                IsSystem = true,
                Remark = $"查询权限",
                Resource = @event.Scopes
            });

            using var db = GetGoldPermissionDB();
            await db.InsertAsync(new MenuPermissionAssociation()
            {
                MenuId = ActorId,
                PermissionId = ActorId
            });

            #endregion

            Logger.LogInformation($"---创建菜单---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(MenuUpdateEvent @event, EventMetadata eventMetadata)
        {
            await Task.CompletedTask;
            Logger.LogInformation($"---更新菜单---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(MenuDeleteEvent @event, EventMetadata eventMetadata)
        {
            await Task.CompletedTask;
            Logger.LogInformation($"---删除菜单---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        #endregion

        #region 配置菜单权限

        /// <summary>
        /// 配置菜单权限
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(MenuConfigurePermissionEvent @event, EventMetadata eventMetadata)
        {
            await Task.CompletedTask;
            Logger.LogInformation($"---配置菜单权限---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        #endregion

        #region 移动菜单

        /// <summary>
        /// 移动菜单
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(MenuMoveEvent @event, EventMetadata eventMetadata)
        {
            using var db = GetGoldPermissionDB();

            var query = db.Menus.Where(x => x.ParentId == ActorId);
            foreach (var item in query)
                await GrainFactory.GetGrain<IMenuGrain>(item.Id).Execute(new MoveMenuCommand() { ParentId = ActorId });

            Logger.LogInformation($"---移动菜单---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        #endregion

        #region 禁用菜单

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(MenuDisableEvent @event, EventMetadata eventMetadata)
        {
            await Task.CompletedTask;
            Logger.LogInformation($"---禁用菜单---FlowGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
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
