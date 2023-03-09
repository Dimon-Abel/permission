using System.Threading.Tasks;
using GoldCloud.Domain.Impls.State;
using GoldCloud.Domain.Interfaces.Commands.Menu;
using GoldCloud.Domain.Interfaces.Domain;
using GoldCloud.Domain.Interfaces.Events.Menu;
using GoldCloud.Infrastructure.Shared.Enumerations;
using GoldCloud.Infrastructure.Shared.Exception;
using KingMetal.Domains.Abstractions.Transaction;
using KingMetal.Domains.Core.Grains;

namespace GoldCloud.Domain.Impls.Grain
{
    /// <summary>
    /// 菜单 Grain
    /// </summary>
    public class MenuGrain : DomainGrain<MenuState, long>, IMenuGrain
    {
        #region 增删改

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task Handler(CreateMenuCommand command) =>
            await RaiseEvent(command.CommandId, new MenuCreateEvent()
            {
                Name = command.Name,
                Path = command.Path,
                ParentId = command.ParentId,
                Order = command.Order,
                Disabled = command.Disabled,
                Hidden = command.Hidden,
                SystemId = command.SystemId,
                Remark = command.Remark,
                ComponentUrl = command.ComponentUrl,
                Meta = command.Meta,
                AlwaysShow = command.AlwaysShow,
                Lang = command.Lang,
                FullPath = await GeneratePathAsync(command.ParentId),
                Scopes = command.Scopes
            });

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task Handler(UpdateMenuCommand command) =>
            await RaiseEvent(command.CommandId, new MenuUpdateEvent()
            {
                Name = command.Name,
                Path = command.Path,
                ParentId = command.ParentId,
                Order = command.Order,
                Disabled = command.Disabled,
                Hidden = command.Hidden,
                SystemId = command.SystemId,
                Remark = command.Remark,
                ComponentUrl = command.ComponentUrl,
                Meta = command.Meta,
                AlwaysShow = command.AlwaysShow,
                Lang = command.Lang,
                FullPath = await GeneratePathAsync(command.ParentId)
            });

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task Handler(DeleteMenuCommand command) =>
            await RaiseEvent(command.CommandId, Mapper<DeleteMenuCommand, MenuDeleteEvent>(command));

        #endregion

        #region 配置菜单权限

        /// <summary>
        /// 配置菜单权限
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task Handler(ConfigurePermissionCommand command, IKingMetalTransaction transaction) =>
            await RaiseEvent(transaction, command.CommandId, new MenuConfigurePermissionEvent() { Permissions = command.Permissions });

        #endregion

        #region 移动菜单

        /// <summary>
        /// 移动菜单
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task Handler(MoveMenuCommand command) =>
            await RaiseEvent(command.CommandId, new MenuMoveEvent() { ParentId = command.ParentId, FullPath = await GeneratePathAsync(command.ParentId) });

        #endregion

        #region 禁用菜单

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task Handler(DisableMenuCommand command) =>
            await RaiseEvent(command.CommandId, new MenuDisableEvent());

        #endregion

        #region private 

        /// <summary>
        /// 生成菜单路径
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<string> GeneratePathAsync(long? parentId)
        {
            var path = $"/{ActorId}";
            if (parentId != null)
            {
                var grain = GrainFactory.GetGrain<IMenuGrain>(parentId.Value);
                if (grain == null)
                    throw new GoldCloudException(ErrorCode.ResourcesNotFound, "上级菜单不存在");
                path = $"{((MenuState)await grain.GetState())?.FullPath}/{ActorId}";
            }

            return await Task.FromResult(path);
        }

        #endregion
    }
}
