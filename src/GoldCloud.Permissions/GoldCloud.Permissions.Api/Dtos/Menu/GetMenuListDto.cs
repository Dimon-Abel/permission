using GoldCloud.Infrastructure.Common.ValueObjects;

namespace GoldCloud.Permissions.Api.Dtos.Menu
{
    /// <summary>
    /// 获取菜单列表Dto
    /// </summary>
    public class GetMenuListDto : PageRequest
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属系统
        /// </summary>
        public long? SystemId { get; set; }

        /// <summary>
        /// 是否禁用    true|禁用~false|未禁用
        /// </summary>
        public bool? Disabled { get; set; }
    }
}
