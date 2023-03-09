using GoldCloud.Infrastructure.Shared.Enumerations;
using System;

namespace GoldCloud.Infrastructure.Shared.Exception
{
    #region 统一异常对象

    /// <summary>
    /// 统一异常对象
    /// </summary>
    public class GoldCloudException : System.Exception
    {
        #region 属性

        /// <summary>
        /// 错误编码
        /// </summary>
        public ErrorCode ErrorCode { get; }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="message">异常描述</param>
        public GoldCloudException(string message) : this(ErrorCode.Unknown, message) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode">错误编码</param>
        public GoldCloudException(ErrorCode errorCode) => ErrorCode = errorCode;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode">错误编码</param>
        /// <param name="message">异常描述</param>
        public GoldCloudException(ErrorCode errorCode, string message) : base(message) => ErrorCode = errorCode;

        #endregion

        #region 异常的字符串形式

        /// <summary>
        /// 异常的字符串形式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => $"{(int)ErrorCode}|{ErrorCode.GetDisplayName()},{Message}";

        #endregion
    }

    #endregion
}
