namespace GoldCloud.Permissions.Api.Options
{
    /// <summary>
    /// 内部认证服务器 JwtBearerOptions
    /// </summary>
    public class InternalJwtBearerOptions
    {
        /// <summary>
        /// 策略名称
        /// </summary>
        public string Policy { get; set; } = "internal_ids4_policy";

        /// <summary>
        /// 认证服务器地址
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// AuthenticationScheme
        /// </summary>
        public string AuthenticationScheme { get; set; } = "internal_ids4";

        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; } = "crm_api";

        /// <summary>
        /// ValidIssuers
        /// </summary>
        public string[] ValidIssuers { get; set; }
    }
}