using System.Security.Claims;
using System.Security.Principal;

#pragma warning disable CS8600 // 将 null 文本或可能的 null 值转换为不可为 null 类型。
#pragma warning disable CS8601 // 将 null 文本或可能的 null 值转换为不可为 null 类型。
#pragma warning disable CS8603 // 将 null 文本或可能的 null 值转换为不可为 null 类型。
#pragma warning disable CS8618 // 将 null 文本或可能的 null 值转换为不可为 null 类型。

namespace GoldCloud.Infrastructure.Common.Extensions
{
    /// <summary>
    /// 身份标识扩展方法
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// 从声明的身份标识中读取用户信息
        /// </summary>
        /// <param name="identity"> </param>
        /// <returns> </returns>
        public static IdentityUserInfo GetUserInfo(this IIdentity identity)
        {
            if (identity == null)
                return default;

            if (identity is not ClaimsIdentity claimsIdentity)
                return default;

            string userId = claimsIdentity.FindFirst("UserId")?.Value;   //主键ID

            if (string.IsNullOrWhiteSpace(userId)) return default;

            string userName = claimsIdentity.FindFirst("UserName")?.Value;  //姓名
            string displayName = claimsIdentity.FindFirst("DisplayName")?.Value;  // 显示名称
            string realName = claimsIdentity.FindFirst("RealName")?.Value;  // 显示名称
            string avatar = claimsIdentity.FindFirst("Avatar")?.Value;     // 头像
            string email = claimsIdentity.FindFirst("Email")?.Value;     // 邮箱
            string phone = claimsIdentity.FindFirst("PhoneNumber")?.Value;   //手机号
            string accountStatus = claimsIdentity.FindFirst("AccountStatus")?.Value;   // 账号状态
            string authenticationStatus = claimsIdentity.FindFirst("AuthenticationStatus")?.Value;   // 认证状态
            string userType = claimsIdentity.FindFirst("UserType")?.Value;   // 认证状态

            return new IdentityUserInfo
            {
                UserId = userId,
                UserName = userName,
                DisplayName = displayName,
                RealName = realName,
                Email = email,
                PhoneNumber = phone,
                Avatar = avatar,
                AccountStatus = accountStatus,
                AuthenticationStatus = authenticationStatus,
                UserType = userType
            };
        }
    }

    /// <summary>
    /// IdentityUserInfo
    /// </summary>
    public class IdentityUserInfo
    {
        /// <summary>
        /// 获取或设置 用户主键
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置 真实名称
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public string AccountStatus { get; set; }

        /// <summary>
        /// 认证状态
        /// </summary>
        public string AuthenticationStatus { get; set; }

        /// <summary>
        /// 获取或设置 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 用户类型 1会员、2员工
        /// </summary>
        public string UserType { get; set; }
    }
}

#pragma warning restore CS8600 // 将 null 文本或可能的 null 值转换为不可为 null 类型。
#pragma warning restore CS8601 // 将 null 文本或可能的 null 值转换为不可为 null 类型。
#pragma warning restore CS8603 // 将 null 文本或可能的 null 值转换为不可为 null 类型。
#pragma warning restore CS8618 // 将 null 文本或可能的 null 值转换为不可为 null 类型。