using System.Collections.Generic;

namespace SeedDataInitialize.Dto
{
    public class ConfigureResourceDto
    {
        /// <summary>
        /// 权限标识
        /// </summary>
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
