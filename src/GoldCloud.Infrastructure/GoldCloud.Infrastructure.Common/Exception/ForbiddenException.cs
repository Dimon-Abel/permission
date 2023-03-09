using GoldCloud.Infrastructure.Common.Enumerations;

namespace GoldCloud.Infrastructure.Common.Exception
{
    /// <summary>
    /// 禁止操作异常
    /// </summary>
    public class ForbiddenException : GoldCloudException
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public ForbiddenException() : this(ErrorCode.Forbidden) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode">错误编码</param>
        public ForbiddenException(ErrorCode errorCode) : base(errorCode) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="message">异常描述</param>
        public ForbiddenException(string message) : base(ErrorCode.InvalidArgument, message) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode">错误编码</param>
        /// <param name="message">异常描述</param>
        public ForbiddenException(ErrorCode errorCode, string message) : base(errorCode, message) { }
    }
}
