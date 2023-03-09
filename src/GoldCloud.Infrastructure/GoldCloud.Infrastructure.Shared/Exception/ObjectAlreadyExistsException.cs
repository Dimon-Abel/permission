using GoldCloud.Infrastructure.Shared.Enumerations;

namespace GoldCloud.Infrastructure.Shared.Exception
{
    #region 对象已经存在异常

    /// <summary>
    /// 对象已经存在异常
    /// </summary>
    public class ObjectAlreadyExistsException : GoldCloudException
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="message">异常消息</param>
        public ObjectAlreadyExistsException(string message) : base(ErrorCode.ObjectAlreadyExists, message) { }

        /// <summary>
        /// 初始化
        /// </summary>
        public ObjectAlreadyExistsException() : base(ErrorCode.ObjectAlreadyExists, "Object already exists") { }
    }

    #endregion
}
