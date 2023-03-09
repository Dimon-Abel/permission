using System.Threading.Tasks;
using GoldCloud.Domain.Impls.State;
using GoldCloud.Domain.Interfaces.Commands.Permission;
using GoldCloud.Domain.Interfaces.Domain;
using GoldCloud.Infrastructure.Shared.Enumerations;
using GoldCloud.Domain.Interfaces.Events.Permission;
using GoldCloud.Infrastructure.Shared.Exception;
using KingMetal.Domains.Abstractions.Transaction;
using KingMetal.Domains.Core.Grains;

namespace GoldCloud.Domain.Impls.Grain
{
    /// <summary>
    /// 权限 Grain
    /// </summary>
    public class PermissionGrain : DomainGrain<PermissionState, long>, IPermissionGrain
    {
        #region 增删改

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="command"><see cref="CreatePermissionCommand" /></param>
        /// <returns></returns>
        public async Task Handler(CreatePermissionCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new GoldCloudException(ErrorCode.InvalidArgument, "权限名称不能为空");
            if (command.Type != PermissionType.Button && command.Type != PermissionType.Menu)
                throw new GoldCloudException(ErrorCode.InvalidArgument, "无效的权限类型");

            await RaiseEvent(command.CommandId, new PermissionCreateEvent()
            {
                Name = command.Name,
                Command = command.Command,
                Order = command.Order,
                ParentId = command.ParentId,
                IsSystem = command.IsSystem,
                Type = command.Type,
                SystemId = command.SystemId,
                Path = await GeneratePathAsync(command.ParentId),
                Resource = command.Resource,
                Remark = command.Remark
            });
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="command"><see cref="UpdatePermissionCommand" /></param>
        /// <returns></returns>
        public async Task Handler(UpdatePermissionCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
                throw new GoldCloudException(ErrorCode.InvalidArgument, "权限名称不能为空");
            if (command.Type != PermissionType.Button && command.Type != PermissionType.Menu)
                throw new GoldCloudException(ErrorCode.InvalidArgument, "无效的权限类型");

            await RaiseEvent(command.CommandId, new PermissionUpdateEvent()
            {
                Name = command.Name,
                Command = command.Command,
                Order = command.Order,
                ParentId = command.ParentId,
                IsSystem = command.IsSystem,
                Type = command.Type,
                SystemId = command.SystemId,
                Path = await GeneratePathAsync(command.ParentId),
                Resource = command.Resource,
                Remark = command.Remark
            });
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="command"><see cref="DeletePermissionCommand" /></param>
        /// <returns></returns>
        public async Task Handler(DeletePermissionCommand command) =>
            await RaiseEvent(command.CommandId, new PermissionDeleteEvent());

        #endregion

        #region 配置角色权限

        /// <summary>
        /// 配置角色权限
        /// </summary>
        /// <param name="command"><see cref="ConfigureRolePermissionCommand" /></param>
        /// <returns></returns>
        public async Task Handler(ConfigureRolePermissionCommand command, IKingMetalTransaction transaction) =>
            await RaiseEvent(transaction, command.CommandId, new RoleConfigurePermissionEvent() { RoleId = command.RoleId, PermissionIds = command.PermissionIds });

        #endregion

        #region 删除角色权限

        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="command"><see cref="DeleteRolePermissionCommand" /></param>
        /// <returns></returns>
        public async Task Handler(DeleteRolePermissionCommand command) =>
            await RaiseEvent(command.CommandId, new RolePermissionDeleteEvent() { RoleId = command.RoleId });

        #endregion

        #region 移动权限

        /// <summary>
        /// 移动权限
        /// </summary>
        /// <param name="command"><see cref="MovePermissionCommand" /></param>
        /// <returns></returns>
        public async Task Handler(MovePermissionCommand command) =>
            await RaiseEvent(command.CommandId, new PermissionMoveEvent() { ParentId = command.ParentId, Path = await GeneratePathAsync(command.ParentId) });

        #endregion


        #region 更新权限资源

        /// <summary>
        /// 更新权限资源
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task Handler(UpdateResourceCommand command) =>
            await RaiseEvent(command.CommandId, new ResourceUpdateEvent() { Resource = command.Resource });

        #endregion

        #region private

        /// <summary>
        /// 生成权限路径
        /// </summary>
        /// <param name="parentId">上级权限标识</param>
        /// <returns></returns>
        public async Task<string> GeneratePathAsync(long? parentId)
        {
            var path = $"/{ActorId}";
            if (parentId != null)
            {
                var grain = GrainFactory.GetGrain<IPermissionGrain>(parentId.Value);
                if (grain == null)
                    throw new GoldCloudException(ErrorCode.ResourcesNotFound, "上级菜单不存在");
                path = $"{((PermissionState)await grain.GetState())?.Path}/{ActorId}";
            }

            return await Task.FromResult(path);
        }

        #endregion
    }
}
