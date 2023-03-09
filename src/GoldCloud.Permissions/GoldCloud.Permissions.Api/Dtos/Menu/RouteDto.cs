using GoldCloud.Infrastructure.Shared.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace GoldCloud.Permissions.Api.Dtos.Menu
{
    /// <summary>
    /// 路由信息
    /// </summary>
    public class RouteDto
    {
        #region 属性

        /// <summary>
        /// 菜单标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 上级菜单标识
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 路由名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 组件地址
        /// </summary>
        public string ComponentUrl { get; set; }

        /// <summary>
        /// 无下级时，是否折叠显示
        /// </summary>
        public bool AlwaysShow { get; set; }

        /// <summary>
        /// 翻译词
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// 转跳地址
        /// </summary>
        public string Redirect
        {
            get
            {
                return Children.Any() ? $"{Path}/{Children.OrderBy(x => x.Order).FirstOrDefault()?.Path}" : "";
            }
        }

        /// <summary>
        /// 菜单排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 路由Meta
        /// </summary>
        public Meta Meta { get; set; }

        /// <summary>
        /// 子路由
        /// </summary>
        public List<RouteDto> Children { get; set; }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public RouteDto()
        {
            Children = new List<RouteDto>();
        }

        #endregion
    }
}
