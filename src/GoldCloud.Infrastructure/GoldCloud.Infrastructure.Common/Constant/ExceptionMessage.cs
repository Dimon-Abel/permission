namespace GoldCloud.Infrastructure.Common.Constant
{
    #region 异常描述消息

    /// <summary>
    /// 异常描述消息
    /// </summary>
    public static class ExceptionMessage
    {
        #region 异常消息定义

        /// <summary>
        /// 值不能小于指定值(value can't less than {0})
        /// </summary>
        public static readonly string ValueCanNotLessThanValue = GetDisplayName(nameof(ValueCanNotLessThanValue));

        /// <summary>
        /// 值不能小于等于指定的值(value can't less than or equal {0})
        /// </summary>
        public static readonly string ValueCanNotLessThanOrEqualValue = GetDisplayName(nameof(ValueCanNotLessThanOrEqualValue));

        /// <summary>
        /// 值不能为null(Value cannot be null)
        /// </summary>
        public static readonly string ValueCanNotNull = GetDisplayName(nameof(ValueCanNotNull));

        /// <summary>
        /// 值不能为空(Value cannot be empty)
        /// </summary>
        public static readonly string ValueCanNotEmpty = GetDisplayName(nameof(ValueCanNotEmpty));

        /// <summary>
        /// Actor重复创建(Id is {0} Actor can't repeat creation)
        /// </summary>
        public static readonly string ActorRepeatCreation = GetDisplayName(nameof(ActorRepeatCreation));

        /// <summary>
        /// 值已经存在(the {0} of {1} has already exist)
        /// </summary>
        public static readonly string ValueAlreadyExistV1 = GetDisplayName(nameof(ValueAlreadyExistV1));

        /// <summary>
        /// 值已经存在(the {0} has already exist)
        /// </summary>
        public static readonly string ValueAlreadyExistV2 = GetDisplayName(nameof(ValueAlreadyExistV2));

        /// <summary>
        /// 不支持的交易类型(Not Support Trading Type {0})
        /// </summary>
        public static readonly string NotSupportTradingType = GetDisplayName(nameof(NotSupportTradingType));

        /// <summary>
        /// 超出范围({0} Out Of Range)
        /// </summary>
        public static readonly string OutOfRange = GetDisplayName(nameof(OutOfRange));

        /// <summary>
        /// 服务不可用
        /// </summary>
        public static readonly string ServiceUnavailable = GetDisplayName(nameof(ServiceUnavailable));

        /// <summary>
        /// 参数不xxx不能为(The parameter {0} cannot be null)
        /// </summary>
        public static readonly string ParameterCannotBeNull = GetDisplayName(nameof(ParameterCannotBeNull));

        /// <summary>
        /// 资源未找到
        /// </summary>
        public static readonly string ResourcesNotFound = GetDisplayName(nameof(ResourcesNotFound));


        #endregion

        #region 获取对应语言的描述信息

        /// <summary>
        /// 获取对应语言的描述信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetDisplayName(string name)
            => Culture.Constant.ExceptionMessage.Language.ResourceManager.GetString(name);

        #endregion
    }

    #endregion
}
