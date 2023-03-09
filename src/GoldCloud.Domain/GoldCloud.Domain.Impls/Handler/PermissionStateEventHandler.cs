using GoldCloud.Domain.Impls.State;
using GoldCloud.Domain.Interfaces.Events.Permission;
using KingMetal.Domains.Core.Handler;

namespace GoldCloud.Domain.Impls.Handler
{
    /// <summary>
    /// 权限状态事件处理器
    /// </summary>
    public class PermissionStateEventHandler : KingMetalStateEventHandler<PermissionState>
    {
        #region 增删改

        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(PermissionState state, PermissionCreateEvent @event)
        {
            state.Name = @event.Name;
            state.Command = @event.Command;
            state.Order = @event.Order;
            state.ParentId = @event.ParentId;
            state.IsSystem = @event.IsSystem;
            state.Type = @event.Type;
            state.SystemId = @event.SystemId;
            state.Path = @event.Path;
            state.Resource = @event.Resource;
            state.Remark = @event.Remark;
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(PermissionState state, PermissionUpdateEvent @event)
        {
            state.Name = @event.Name;
            state.Command = @event.Command;
            state.Order = @event.Order;
            state.ParentId = @event.ParentId;
            state.IsSystem = @event.IsSystem;
            state.Type = @event.Type;
            state.SystemId = @event.SystemId;
            state.Path = @event.Path;
            state.Resource = @event.Resource;
            state.Remark = @event.Remark;
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(PermissionState state, PermissionDeleteEvent @event) { }

        #endregion

        #region 配置角色权限

        /// <summary>
        /// 配置角色权限
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(PermissionState state, RoleConfigurePermissionEvent @event) { }

        #endregion

        #region 删除角色权限

        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(PermissionState state, RolePermissionDeleteEvent @event) { }

        #endregion

        #region 移动权限

        /// <summary>
        /// 移动权限
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(PermissionState state, PermissionMoveEvent @event)
        {
            state.ParentId = @event.ParentId;
            state.Path = @event.Path;
        }

        #endregion

        #region 更新权限资源

        /// <summary>
        /// 更新权限资源
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(PermissionState state, ResourceUpdateEvent @event)
        {
            state.Resource = @event.Resource;
        }

        #endregion
    }
}
