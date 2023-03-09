using GoldCloud.Infrastructure.Common.Enumerations;

namespace GoldCloud.Infrastructure.Common.Exception
{
    #region 修改没有发生变化异常

    /// <summary>
    /// 修改没有发生变化异常
    /// </summary>
    public class NoChangeException : GoldCloudException
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="message">异常消息</param>
        public NoChangeException(string message) : base(ErrorCode.NoChange, message) { }

        /// <summary>
        /// 初始化
        /// </summary>
        public NoChangeException() : base(ErrorCode.NoChange, "修改没有发生变化") { }
    }

    #endregion 修改没有发生变化异常
}