using GoldCloud.Infrastructure.Common.ValueObjects;

namespace GoldCloud.Permissions.Api.Dtos
{
    /// <summary>
    /// 分页查询 ApiResource  Dto
    /// </summary>
    public class GetResourceListDto : PageRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
