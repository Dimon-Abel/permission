namespace GoldCloud.Library.Interceptor.Models
{
    /// <summary>
    /// API 请求结果
    /// </summary>
    public class HttpResultDto<T>
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 接口版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 数据对象
        /// </summary>
        public T Content { get; set; }
    }
}
