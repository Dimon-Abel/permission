namespace GoldCloud.Permissions.Api.Dtos.Permission
{
    /// <summary>
    /// 移动权限 Dto
    /// </summary>
    public class MovePermissionDto
    {
        /// <summary>
        /// 权限标识
        /// </summary>
        public long PermissionId { get; set; }
        /// <summary>
        /// 上级权限标识
        /// </summary>
        public long? ParentId { get; set; }
    }
}
