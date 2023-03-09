using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoldCloud.Permissions.Api.Dtos.Permission
{
    /// <summary>
    /// 配置权限资源
    /// </summary>
    public class ConfigureResourceDto
    {
        /// <summary>
        /// 权限标识
        /// </summary>
        [Required]
        public long PrivilegeId { get; set; }

        /// <summary>
        /// Scope Name
        /// </summary>
        public List<string> ScopeNames { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public ConfigureResourceDto()
        {
            ScopeNames = new List<string>();
        }
    }
}
