namespace GoldCloud.Permissions.Api.Dtos
{
    /// <summary>
    /// Api Resource
    /// </summary>
    public class ApiResourceDto
    {
        /// <summary>
        /// 标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 资源说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long Created { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public long Updated { get; set; }
    }
}
