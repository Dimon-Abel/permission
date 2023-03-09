using System.ComponentModel.DataAnnotations;

namespace GoldCloud.Permissions.Api.Dtos
{
    /// <summary>
    /// 删除权限
    /// </summary>
    public class RemovePermissionDto
    {
        /// <summary>
        /// 权限标识
        /// </summary>
        [Required]
        public long Id { get; set; }
    }
}
