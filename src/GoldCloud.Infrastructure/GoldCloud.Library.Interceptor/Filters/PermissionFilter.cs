using GoldCloud.Library.Interceptor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Caching;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoldCloud.Library.Interceptor.Filters
{
    /// <summary>
    /// 用户角色权限过滤器
    /// </summary>
    public class PermissionFilter : IAsyncActionFilter
    {
        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public PermissionFilter() { }

        #endregion

        #region Action执行之后的事件

        /// <summary>
        /// Action执行之后的事件
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        #endregion

        #region Action执行前校验用户角色权限

        /// <summary>
        /// Action执行前校验用户角色权限
        /// </summary>
        /// <param name="context"></param>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;

            #region 匿名控制器或匿名行为直接放行

            if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            {
                await next();
                return;
            }

            #endregion

            #region 获取当前请求资源地址

            var resourcePath = $"{context.ActionDescriptor.RouteValues["controller"].Replace("/", ".")}.{context.ActionDescriptor.RouteValues["action"].Replace("/", ".")}".ToLower();

            #endregion

            #region 获取Token

            var headers = httpContext.Request.Headers;
            string token = string.Empty;
            if (headers.TryGetValue("Authorization", out var authorization))
                token = authorization.ToString().Replace("Bearer ", "");

            #endregion

            if (!string.IsNullOrWhiteSpace(token))
            {
                #region 解析Token 判断用户是否有权限请求资源

                var roleIds = new List<string>();
                JwtSecurityToken jst = new JwtSecurityTokenHandler().ReadJwtToken(token);

                var claims = jst.Claims;
                if (claims.Any(x => x.Type == "UserRoles"))
                {
                    #region 用户角色权限验证

                    foreach (var claim in claims.Where(x => x.Type == "UserRoles").Select(x => x.Value))
                    {
                        var role = JsonSerializer.Deserialize<UserRoleDto>(claim);
                        roleIds.Add(role.Id);
                    }

                    if (roleIds.Any())
                    {
                        #region 角色-权限 缓存

                        ObjectCache cache = MemoryCache.Default;
                        var cacheValue = cache.Get(string.Join("", roleIds));
                        IEnumerable<PermissionDto> permissions = cacheValue is not null ? cacheValue as IEnumerable<PermissionDto> :
                            await GetRolePermission($"{httpContext.Request.Scheme}://{httpContext.Request.Host.Value}", roleIds);

                        if (permissions is not null && permissions.Any())
                            cache.Set(string.Join("", roleIds), permissions, DateTimeOffset.Now.AddSeconds(150));

                        #endregion

                        foreach (var item in permissions)
                        {
                            if (item.Resources.Any(x => x.ResourcePath.ToLower() == resourcePath))
                            {
                                await next();
                                return;
                            }
                        }
                    }

                    #endregion
                }
                else if (claims.Any(x => x.Type == "scope"))
                {
                    #region 作用域权限验证

                    var scopes = claims.Where(x => x.Type == "scope").Select(x => x.Value.ToLower());
                    if (scopes.Any(x => scopes.Contains(resourcePath)))
                    {
                        await next();
                        return;
                    }

                    #endregion
                }

                #endregion
            }

            context.Result = new UnauthorizedResult();
        }

        #endregion

        #region 获取角色权限

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="host">域名</param>
        /// <param name="roleIds">角色标识集合</param>
        /// <returns></returns>
        private async Task<IEnumerable<PermissionDto>> GetRolePermission(string host, List<string> roleIds)
        {
            var pord = $"{host}/api/permission/v1/Privilege/role/permission?roleIds={string.Join("&roleIds=", roleIds)}";
            var client = new RestClient(pord);
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteAsync<HttpResultDto<IEnumerable<PermissionDto>>>(request);
            return await Task.FromResult(response.Data.Content);
        }

        #endregion
    }
}
