using GoldCloud.Infrastructure.DataBase.Entities;

namespace GoldCloud.Permissions.Api.Dtos
{
    /// <summary>
    /// Api Scope
    /// </summary>
    public class ApiScopeDto
    {
        /// <summary>
        /// 标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Api资源标识
        /// </summary>
        public long ApiResourceId { get; set; }

        /// <summary>
        /// Api Resource
        /// </summary>
        public ApiResource ApiResourceEntity { get; set; }
    }
}
