using Microsoft.AspNetCore.Authorization;

namespace GoldCloud.Permissions.Api.Handlers
{
    #region Api Token验证的策略

    /// <summary>
    /// Api Token验证的策略
    /// </summary>
    public class ValidTokenStateRequirement : IAuthorizationRequirement
    {
    }

    #endregion
}
