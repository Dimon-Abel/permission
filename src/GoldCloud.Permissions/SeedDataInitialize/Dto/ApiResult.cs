namespace SeedDataInitialize.Dto
{
    public class ApiResult<T>
    {
        /// <summary>
        /// 请求响应数据
        /// </summary>
        public T Content { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 版本标识
        /// </summary>
        public string Version { get; set; }
    }
}
