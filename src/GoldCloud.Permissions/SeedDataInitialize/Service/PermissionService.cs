using Microsoft.Extensions.Configuration;
using RestSharp;
using SeedDataInitialize.Dto;
using System;
using System.Linq;

namespace SeedDataInitialize.Service
{
    public class PermissionService
    {
        #region 属性

        /// <summary>
        /// http client
        /// </summary>
        static RestClient client;

        #endregion

        public PermissionService()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                  .SetBasePath(Environment.CurrentDirectory)
                  .AddJsonFile("appsetting.json", optional: false, reloadOnChange: true)
                  .Build();

            var host = configuration.GetSection("host");
            if (host == null)
                throw new Exception("未成功加载host地址");
            client = new RestClient(host.Value);
        }

        #region 创建系统

        /// <summary>
        /// 创建系统
        /// </summary>
        /// <param name="system">系统信息</param>
        /// <returns></returns>
        public ApiResult<object> CreateSystem(CreateSystemDto system)
        {
            var request = new RestRequest($"/api/permission/system/create").AddJsonBody(system);
            var response = client.Post<ApiResult<object>>(request);
            return response;
        }

        #endregion

        #region 创建菜单

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="menu">菜单信息</param>
        /// <returns></returns>
        public ApiResult<object> CreateMenu(CreateMenuDto menu)
        {
            var request = new RestRequest($"/menu/create").AddJsonBody(menu);
            var response = client.Post<ApiResult<object>>(request);
            return response;
        }

        #endregion

        #region 创建权限

        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="permission">权限信息</param>
        /// <returns></returns>
        public ApiResult<object> CreatePrivilege(CreatePermissionDto permission)
        {
            var request = new RestRequest($"/privilege/createNoVerify").AddJsonBody(permission );
            var response = client.Post<ApiResult<object>>(request);
            return response;
        }

        #endregion

        #region 配置权限资源

        /// <summary>
        /// 配置权限资源
        /// </summary>
        /// <param name="permission">权限信息</param>
        /// <returns></returns>
        public ApiResult<object> ConfigureResource(ConfigureResourceDto dto)
        {
            var request = new RestRequest($"/privilege/configure/resource").AddJsonBody(dto);
            var response = client.Post<ApiResult<object>>(request);
            return response;
        }

        #endregion

        #region 关联菜单-权限

        /// <summary>
        /// 配置菜单权限
        /// </summary>
        /// <param name="menuId">菜单标识</param>
        /// <param name="privilegeIds">权限标识集合</param>
        /// <returns></returns>
        public bool Configure(long menuId, params long[] privilegeIds)
        {
            var request = new RestRequest($"/menu/configureNoVerify").AddJsonBody(new { MenuId = menuId, PermissionIds = privilegeIds.ToList() });
            var response = client.Post<ApiResult<object>>(request);
            return response.ErrorCode == 2000;
        }

        #endregion
    }
}
