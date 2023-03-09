using GoldCloud.Infrastructure.Common.ValueObjects;

namespace GoldCloud.Permissions.Api.Dtos
{
    /// <summary>
    /// 分页查询 Scope Dto
    /// </summary>
    public class GetScopeListDto : PageRequest
    {
        /// <summary>
        /// Scope 标识
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Resource 标识
        /// </summary>
        public long? ApiResourceId { get; set; }
    }
}
