using System.ComponentModel.DataAnnotations;
using Resources = GoldCloud.Infrastructure.Culture.Enumerations.ErrorCode;

namespace GoldCloud.Infrastructure.Shared.Enumerations
{
    #region 错误编码

    /// <summary>
    /// 错误编码
    /// </summary>
    public enum ErrorCode : ushort
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Display(Name = nameof(Success), ResourceType = typeof(Resources.Language))]
        Success = 0,

        /// <summary>
        /// 未知错误
        /// </summary>
        [Display(Name = nameof(Unknown), ResourceType = typeof(Resources.Language))]
        Unknown = 1000,

        /// <summary>
        /// 无效参数
        /// </summary>
        [Display(Name = nameof(InvalidArgument), ResourceType = typeof(Resources.Language))]
        InvalidArgument = 2001,

        /// <summary>
        /// 未授权
        /// </summary>
        [Display(Name = nameof(Unauthorized), ResourceType = typeof(Resources.Language))]
        Unauthorized = 2002,

        /// <summary>
        /// 禁止操作
        /// </summary>
        [Display(Name = nameof(Forbidden), ResourceType = typeof(Resources.Language))]
        Forbidden = 2003,

        /// <summary>
        /// 资源未找到
        /// </summary>
        [Display(Name = nameof(ResourcesNotFound), ResourceType = typeof(Resources.Language))]
        ResourcesNotFound = 2004,

        /// <summary>
        /// 服务不可用
        /// </summary>
        [Display(Name = nameof(ServiceUnavailable), ResourceType = typeof(Resources.Language))]
        ServiceUnavailable = 2005,

        /// <summary>
        /// 参数异常
        /// </summary>
        [Display(Name = nameof(ArgumentException), ResourceType = typeof(Resources.Language))]
        ArgumentException = 2006,

        /// <summary>
        /// 对象不能初始化
        /// </summary>
        [Display(Name = nameof(ObjectCannotInitialized), ResourceType = typeof(Resources.Language))]
        ObjectCannotInitialized = 2007,

        /// <summary>
        /// 对象已经存在
        /// </summary>
        [Display(Name = nameof(ObjectAlreadyExists), ResourceType = typeof(Resources.Language))]
        ObjectAlreadyExists = 2008,

        /// <summary>
        /// 超出范围
        /// </summary>
        [Display(Name = nameof(OutOfRange), ResourceType = typeof(Resources.Language))]
        OutOfRange = 2010,

        /// <summary>
        /// 没有发生变化
        /// </summary>
        [Display(Name = nameof(NoChange), ResourceType = typeof(Resources.Language))]
        NoChange = 2030,
    }

    #endregion
}
