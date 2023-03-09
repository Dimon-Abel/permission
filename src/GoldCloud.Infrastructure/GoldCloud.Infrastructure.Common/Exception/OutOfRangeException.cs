using GoldCloud.Infrastructure.Common.Constant;
using GoldCloud.Infrastructure.Common.Enumerations;

namespace GoldCloud.Infrastructure.Common.Exception
{
    #region 超出限制范围异常

    /// <summary>
    /// 超出限制范围异常
    /// </summary>
    public class OutOfRangeException : GoldCloudException
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public OutOfRangeException() : this(ErrorCode.OutOfRange) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode">异常编码</param>
        public OutOfRangeException(ErrorCode errorCode) : base(errorCode, string.Format(ExceptionMessage.OutOfRange, "")) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="message">异常描述</param>
        public OutOfRangeException(string message) : base(ErrorCode.OutOfRange, message) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode">异常编码</param>
        /// <param name="message">异常描述</param>
        public OutOfRangeException(ErrorCode errorCode, string message) : base(errorCode, message) { }
    }

    #endregion
}
