using GoldCloud.Domain.Interfaces.Events.Menu;
using GoldCloud.Domain.Interfaces.Handler;
using GoldCloud.Infrastructure.DataBase;
using GoldCloud.Infrastructure.DataBase.Entities;
using GoldCloud.Infrastructure.DataBase.Extensions;
using KingMetal.Domains.Abstractions.Event;
using KingMetal.Domains.Core.Grains;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GoldCloud.Domain.Handlers
{
    /// <summary>
    /// 菜单 DB Grain
    /// </summary>
    public class MenuDbGrain : ObserverGrain<long>, IMenuDbGrain
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
            using var db = GetGoldPermissionDB();
            await db.InsertAsync(Mapper<MenuCreateEvent, Menu>(@event, x =>
                x.AfterMap((evt, entity) =>
                    entity.Id = ActorId
                )));

            Logger.LogInformation($"---创建菜单---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(MenuUpdateEvent @event, EventMetadata eventMetadata)
        {
            using var db = GetGoldPermissionDB();
            await db.Menus.Where(x => x.Id == ActorId)
                .Set(x => x.Name, @event.Name)
                .Set(x => x.Path, @event.Path)
                .Set(x => x.ParentId, @event.ParentId)
                .Set(x => x.Order, @event.Order)
                .Set(x => x.Disabled, @event.Disabled)
                .Set(x => x.Hidden, @event.Hidden)
                .Set(x => x.SystemId, @event.SystemId)
                .Set(x => x.ComponentUrl, @event.ComponentUrl)
                .Set(x => x.Remark, @event.Remark)
                .Set(x => x.Meta, @event.Meta)
                .Set(x => x.AlwaysShow, @event.AlwaysShow)
                .Set(x => x.Lang, @event.Lang)
                .Set(x => x.FullPath, @event.FullPath)
                .UpdateAsync();

            Logger.LogInformation($"---更新菜单---DbGrain---{@event.GetDefaultName()}---事件处理, ActorId:{ActorId},Version:{eventMetadata.Version}");
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="event"></param>
        /// <param name="eventMetadata"></param>
        /// <returns></returns>
        public async Task Handler(MenuDeleteEvent @event, EventMetadata eventMetadata)
        {
            using var db = GetGoldPermissionDB();
            await db.Menus.Where(x => x.Id == ActorId).DeleteAsync();

            Logger.LogInformation($"---删除菜单---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
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
            using var db = GetGoldPermissionDB();

            await db.MenuPermissionAssociations.Where(x => x.MenuId == ActorId).DeleteAsync();
            var data = @event.Permissions.Select(x => new MenuPermissionAssociation()
            {
                MenuId = ActorId,
                PermissionId = x.Id
            }).ToList();

            await db.BulkCopyAsync(data);

            Logger.LogInformation($"---配置菜单权限---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
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

            await db.Menus.Where(x => x.Id == ActorId)
                .Set(x => x.ParentId, @event.ParentId)
                .Set(x => x.FullPath, @event.FullPath)
                .UpdateAsync();

            Logger.LogInformation($"---移动菜单---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
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
            using var db = GetGoldPermissionDB();
            await db.Menus.Where(x => x.Id == ActorId)
                .Set(x => x.Disabled, @event.Disable)
                .UpdateAsync();

            Logger.LogInformation($"---禁用菜单---DbGrain---{@event.GetDefaultName()}---事件处理,ActorId:{ActorId},Version:{eventMetadata.Version}");
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
