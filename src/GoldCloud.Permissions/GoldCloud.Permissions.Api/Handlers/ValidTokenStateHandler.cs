using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GoldCloud.Permissions.Api.Handlers
{
    #region Token验证的策略要求的处理器

    /// <summary>
    /// Token验证的策略要求的处理器
    /// </summary>
    public class ValidTokenStateHandler : AuthorizationHandler<ValidTokenStateRequirement>
    {
        #region 私有变量

        /// <summary>
        /// 日志对象
        /// </summary>
        private readonly ILogger logger;

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="logger"></param>

        public ValidTokenStateHandler(ILogger<ValidTokenStateHandler> logger)
            => this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        #endregion

        #region 验证taoken

        /// <summary>
        /// 验证taoken
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidTokenStateRequirement requirement)
        {
            await Task.Yield();


            context.Succeed(requirement);
        }

        #endregion
    }

    #endregion
}
