using Microsoft.AspNetCore.Http;

namespace GoldCloud.Permissions.Api.Extensions
{
    #region HttpContext扩展方法

    /// <summary>
    /// HttpContext扩展方法
    /// </summary>
    public static class HttpContextExtensions
    {
        #region 获取客户端IP

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetRequestIp(this HttpContext httpContext)
        {
            if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                return httpContext.Request.Headers["X-Forwarded-For"].ToString();

            if (httpContext.Request.Headers.ContainsKey("X-Real-IP"))
                return httpContext.Request.Headers["X-Real-IP"].ToString();

            return httpContext.Connection.RemoteIpAddress.ToString();
        }

        #endregion 获取客户端IP

        #region 获取域名

        /// <summary>
        /// 获取域名
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetRequestDomain(this HttpContext httpContext)
        {
            return httpContext.Request.Scheme + "://" + httpContext.Request.Host.Value;
        }

        #endregion 获取域名
    }

    #endregion HttpContext扩展方法
}