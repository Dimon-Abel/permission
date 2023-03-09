using System.Collections.Generic;

namespace SeedDataInitialize.Dto
{
    /// <summary>
    /// 种子数据结构
    /// </summary>
    public class MenuPrivilegeInfo : CreateMenuDto
    {
        /// <summary>
        /// 权限数据集合
        /// </summary>
        public List<CreatePermissionDto> Privileges { get; set; }

        /// <summary>
        /// 资源标识
        /// </summary>
        public List<string> Resource { get; set; }

        /// <summary>
        /// 子级菜单
        /// </summary>
        public List<MenuPrivilegeInfo> Children { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public MenuPrivilegeInfo()
        {
            Privileges = new List<CreatePermissionDto>();
            Resource = new List<string>();
            Children = new List<MenuPrivilegeInfo>();
        }
    }
}
