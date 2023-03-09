using GoldCloud.Infrastructure.Common.Constant;
using GoldCloud.Infrastructure.Common.Enumerations;

namespace GoldCloud.Infrastructure.Common.Exception
{
    #region 服务不可用异常

    /// <summary>
    /// 服务不可用异常
    /// </summary>
    public class ServiceUnavailableException : GoldCloudException
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public ServiceUnavailableException() : this(ErrorCode.ServiceUnavailable) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode"> 异常编码 </param>
        public ServiceUnavailableException(ErrorCode errorCode) : base(errorCode, string.Format(ExceptionMessage.ServiceUnavailable, "")) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="message"> 异常描述 </param>
        public ServiceUnavailableException(string message) : base(ErrorCode.ServiceUnavailable, message) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode"> 异常编码 </param>
        /// <param name="message">   异常描述 </param>
        public ServiceUnavailableException(ErrorCode errorCode, string message) : base(errorCode, message) { }
    }

    #endregion 服务不可用异常
}