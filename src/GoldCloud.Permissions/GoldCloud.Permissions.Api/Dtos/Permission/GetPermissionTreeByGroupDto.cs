namespace GoldCloud.Permissions.Api.Dtos.Permission
{
    /// <summary>
    /// 获取所属系统权限树 Dto
    /// </summary>
    public class GetPermissionTreeByGroupDto
    {
        /// <summary>
        /// 归属系统
        /// </summary>
        public long? SystemId { get; set; }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        public string KeyWord { get; set; }
    }
}
