using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldCloud.Permissions.Api.Dtos.Permission
{
    /// <summary>
    /// 配置角色权限Dto
    /// </summary>
    public class ConfigureRolePermissionDto
    {
        /// <summary>
        /// 角色标识
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 权限标识集合
        /// </summary>
        public List<long> PermissionIds { get; set; }

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public ConfigureRolePermissionDto()
        {
            PermissionIds = new List<long>();
        }

        #endregion
    }
}
