namespace GoldCloud.Permissions.Api.Dtos.Menu
{
    /// <summary>
    /// 移动菜单 Dto
    /// </summary>
    public class MoveMenuDto
    {
        /// <summary>
        /// 菜单标识
        /// </summary>
        public long MenuId { get; set; }
        /// <summary>
        /// 新上级菜单标识
        /// </summary>
        public long? ParentId { get; set; }
    }
}
