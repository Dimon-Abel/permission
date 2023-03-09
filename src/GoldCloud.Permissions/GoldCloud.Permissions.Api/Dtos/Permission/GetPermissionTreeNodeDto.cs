namespace GoldCloud.Permissions.Api.Dtos.Permission
{
    /// <summary>
    /// 获取权限数据树Dto
    /// </summary>
    public class GetPermissionTreeNodeDto
    {
        /// <summary>
        /// 上级标识
        /// </summary>
        public long? ParentId { get; set; } = null;

        /// <summary>
        /// 懒加载
        /// </summary>
        public bool Lazy { get; set; } = false;
    }
}
