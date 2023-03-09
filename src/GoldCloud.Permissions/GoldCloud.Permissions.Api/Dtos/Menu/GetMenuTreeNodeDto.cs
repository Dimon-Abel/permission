namespace GoldCloud.Permissions.Api.Dtos.Menu
{
    /// <summary>
    /// 获取菜单树 Dto
    /// </summary>
    public class GetMenuTreeNodeDto
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
