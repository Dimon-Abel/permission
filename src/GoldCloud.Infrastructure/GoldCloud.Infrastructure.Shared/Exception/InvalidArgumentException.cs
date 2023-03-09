using GoldCloud.Infrastructure.Shared.Enumerations;

namespace GoldCloud.Infrastructure.Shared.Exception
{
    #region 无效参数异常

    /// <summary>
    /// 无效参数异常
    /// </summary>
    public class InvalidArgumentException : GoldCloudException
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public InvalidArgumentException() : this(ErrorCode.InvalidArgument) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode"> 错误编码 </param>
        public InvalidArgumentException(ErrorCode errorCode) : base(errorCode) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="message"> 异常描述 </param>
        public InvalidArgumentException(string message) : base(ErrorCode.InvalidArgument, message) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode"> 错误编码 </param>
        /// <param name="message">   异常描述 </param>
        public InvalidArgumentException(ErrorCode errorCode, string message) : base(errorCode, message) { }
    }

    #endregion 无效参数异常
}