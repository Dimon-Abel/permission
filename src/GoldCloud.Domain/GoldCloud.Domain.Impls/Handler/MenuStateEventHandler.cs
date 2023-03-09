using GoldCloud.Domain.Impls.State;
using GoldCloud.Domain.Interfaces.Events.Menu;
using KingMetal.Domains.Core.Handler;

namespace GoldCloud.Domain.Impls.Handler
{
    /// <summary>
    /// 菜单状态事件处理器
    /// </summary>
    public class MenuStateEventHandler : KingMetalStateEventHandler<MenuState>
    {
        #region 增删改

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(MenuState state, MenuCreateEvent @event)
        {
            state.Name = @event.Name;
            state.Path = @event.Path;
            state.ParentId = @event.ParentId;
            state.Order = @event.Order;
            state.Disabled = @event.Disabled;
            state.Hidden = @event.Hidden;
            state.SystemId = @event.SystemId;
            state.ComponentUrl = @event.ComponentUrl;
            state.Remark = @event.Remark;
            state.Meta = @event.Meta;
            state.AlwaysShow = @event.AlwaysShow;
            state.Lang = @event.Lang;
            state.FullPath = @event.FullPath;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(MenuState state, MenuUpdateEvent @event)
        {
            state.Name = @event.Name;
            state.Path = @event.Path;
            state.ParentId = @event.ParentId;
            state.Order = @event.Order;
            state.Disabled = @event.Disabled;
            state.Hidden = @event.Hidden;
            state.SystemId = @event.SystemId;
            state.ComponentUrl = @event.ComponentUrl;
            state.Remark = @event.Remark;
            state.Meta = @event.Meta;
            state.AlwaysShow = @event.AlwaysShow;
            state.Lang = @event.Lang;
            state.FullPath = @event.FullPath;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(MenuState state, MenuDeleteEvent @event) { }

        #endregion

        #region 配置菜单权限

        /// <summary>
        /// 配置菜单权限
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(MenuState state, MenuConfigurePermissionEvent @event) { }

        #endregion

        #region 移动菜单

        public void Handler(MenuState state, MenuMoveEvent @event)
        {
            state.ParentId = @event.ParentId;
            state.FullPath = @event.FullPath;
        }

        #endregion

        #region 禁用菜单

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="state"></param>
        /// <param name="event"></param>
        public void Handler(MenuState state, MenuDisableEvent @event)
        {
            state.Disabled = @event.Disable;
        }

        #endregion
    }
}
