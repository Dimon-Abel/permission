using System.ComponentModel.DataAnnotations;

namespace GoldCloud.Infrastructure.Shared.Enumerations
{
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionType : byte
    {
        /// <summary>
        /// 菜单
        /// </summary>
        [Display(Name = "Menu")]
        Menu = 1,
        /// <summary>
        /// 按钮
        /// </summary>
        [Display(Name = "Button")]
        Button = 2
    }
}
