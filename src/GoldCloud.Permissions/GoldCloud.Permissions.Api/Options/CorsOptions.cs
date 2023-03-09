namespace GoldCloud.Permissions.Api.Options
{
    /// <summary>
    /// 跨域配置
    /// </summary>
    public class CorsOptions
    {
        /// <summary>
        /// 允许跨域的域名
        /// </summary>
        public string[] Origins { get; set; }
    }
}