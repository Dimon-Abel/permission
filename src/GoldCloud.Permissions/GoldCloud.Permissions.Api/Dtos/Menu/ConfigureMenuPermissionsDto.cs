using System.Collections.Generic;

namespace GoldCloud.Permissions.Api.Dtos.Menu
{
    /// <summary>
    /// 配置菜单权限Dto
    /// </summary>
    public class ConfigureMenuPermissionsDto
    {
        /// <summary>
        /// 菜单标识
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 权限标识
        /// </summary>
        public List<long> PermissionIds { get; set; }

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public ConfigureMenuPermissionsDto()
        {
            PermissionIds = new List<long>();
        }

        #endregion
    }
}
