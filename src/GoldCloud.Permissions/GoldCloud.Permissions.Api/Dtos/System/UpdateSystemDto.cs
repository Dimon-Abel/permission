using System.ComponentModel.DataAnnotations;

namespace GoldCloud.Permissions.Api.Dtos
{
    /// <summary>
    /// 更新权限
    /// </summary>
    public class UpdateSystemDto
    {
        #region 属性

        /// <summary>
        /// 系统标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [Required(ErrorMessage = "系统名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 系统说明
        /// </summary>
        public string Remark { get; set; }

        #endregion
    }
}
