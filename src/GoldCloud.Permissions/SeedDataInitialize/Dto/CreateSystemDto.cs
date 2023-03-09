using System.Collections.Generic;

namespace SeedDataInitialize.Dto
{
    public class CreateSystemDto
    {
        #region 属性

        /// <summary>
        /// 系统标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 系统说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public List<MenuPrivilegeInfo> Menus { get; set; }

        #endregion
    }
}
