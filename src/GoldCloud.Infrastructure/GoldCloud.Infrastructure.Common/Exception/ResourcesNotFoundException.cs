using GoldCloud.Infrastructure.Common.Constant;
using GoldCloud.Infrastructure.Common.Enumerations;

namespace GoldCloud.Infrastructure.Common.Exception
{
    /// <summary>
    /// 资源未找到异常
    /// </summary>
    public class ResourcesNotFoundException : GoldCloudException
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public ResourcesNotFoundException() : this(ErrorCode.ResourcesNotFound) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode">异常编码</param>
        public ResourcesNotFoundException(ErrorCode errorCode) : base(errorCode, string.Format(ExceptionMessage.ResourcesNotFound, "")) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="message">异常描述</param>
        public ResourcesNotFoundException(string message) : base(ErrorCode.ResourcesNotFound, message) { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="errorCode">异常编码</param>
        /// <param name="message">异常描述</param>
        public ResourcesNotFoundException(ErrorCode errorCode, string message) : base(errorCode, message) { }
    }
}
