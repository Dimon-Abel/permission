using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SeedDataInitialize.Dto;
using SeedDataInitialize.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace SeedDataInitialize.Logisc
{
    public class SyncLogisc
    {
        #region 属性

        /// <summary>
        /// 权限Api服务
        /// </summary>
        private PermissionService service;

        /// <summary>
        /// 系统信息数据
        /// </summary>
        private List<CreateSystemDto> systemEntities = new List<CreateSystemDto>();

        #endregion

        #region 初始化

        public SyncLogisc()
        {
            service = new PermissionService();
            LoadData();
        }

        #endregion

        #region 加载种子数据

        private void LoadData()
        {
            Console.WriteLine("加载种子数据 开始...");

            systemEntities.Clear();
            var paths = Directory.GetFiles(Environment.CurrentDirectory, "*-privilege.json");
            foreach (var path in paths)
            {
                var fileName = path.Substring(path.LastIndexOf("\\")).Replace("\\", "");
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile(fileName, optional: false, reloadOnChange: true)
                    .Build();

                var system = (CreateSystemDto)configuration.GetSection("system").Get(typeof(CreateSystemDto));
                if (system != null) systemEntities.Add(system);
            }

            Console.WriteLine($"加载种子数据 完成. 总数:{systemEntities.Count}...");
        }

        #endregion

        #region 写入数据

        /// <summary>
        /// 写入数据
        /// </summary>
        public void Write()
        {
            Console.WriteLine("写入种子数据 开始...");

            foreach (var system in systemEntities)
            {
                var systemResult = service.CreateSystem(system);
                if (systemResult.ErrorCode == 2000)
                {
                    var systemId = long.Parse(systemResult.Content.ToString());
                    int menuCount = 0;

                    foreach (var menu in system.Menus)
                    {
                        menu.SystemId = systemId;
                        menu.Order = menuCount * 1000 + 10;

                        if (menu.Resource.Any())
                            menu.ScopeNames = GetScopeNames(menu.Resource);

                        var result = service.CreateMenu(menu);
                        if (result.ErrorCode == 2000)
                        {
                            var menuId = long.Parse(result.Content.ToString());

                            if (menu.Children.Any())
                            {
                                int index = 0;
                                foreach (var item in menu.Children)
                                {
                                    var order = int.Parse($"{menu.Order}{index * 10 + 10}");
                                    Recursion(item, menuId, order, systemId);
                                    index++;
                                }
                            }

                            if (menu.Privileges.Any())
                            {
                                WritePrivileges(menu.Privileges, menuId, menu.Order, systemId);
                            }

                            menuCount++;
                        }
                    }
                }
            }

            Console.WriteLine("写入种子数据 完成...");

            systemEntities.Clear();
        }

        #endregion

        #region private

        /// <summary>
        /// 写入菜单权限数据
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="parentId"></param>
        /// <param name="menuCount"></param>
        /// <param name="privilegeCount"></param>
        private void Recursion(MenuPrivilegeInfo menu, long parentId, int order, long systemId)
        {
            menu.SystemId = systemId;
            menu.ParentId = parentId;
            menu.Order = order;
            if (string.IsNullOrWhiteSpace(menu.Lang))
                menu.Lang = menu.Name;

            if (menu.Resource.Any())
                menu.ScopeNames = GetScopeNames(menu.Resource);

            var result = service.CreateMenu(menu);
            if (result.ErrorCode == 2000)
            {
                var menuId = long.Parse(result.Content.ToString());
                if (menu.Children.Any())
                {
                    int index = 0;
                    foreach (var item in menu.Children)
                    {
                        var childOrder = int.Parse($"{menu.Order}{index * 10 + 10}");
                        Recursion(item, menuId, childOrder, systemId);
                        index++;
                    }
                }

                if (menu.Privileges.Any())
                {
                    WritePrivileges(menu.Privileges, menuId, order, systemId);
                }
            }
        }

        /// <summary>
        /// 写入权限数据
        /// </summary>
        /// <param name="privileges"></param>
        /// <param name="menuId"></param>
        /// <param name="privilegeCount"></param>
        private void WritePrivileges(List<CreatePermissionDto> privileges, long menuId, int order, long systemId)
        {
            //List<long> privilegeIds = new List<long>();
            int index = 0;
            foreach (var privilege in privileges)
            {
                privilege.SystemId = systemId;
                privilege.Order = int.Parse($"{order}{index * 10 + 10}");
                privilege.ParentId = menuId;
                privilege.ScopeNames = GetScopeNames(privilege.ScopeNames);

                var privilegeResult = service.CreatePrivilege(privilege);
                if (privilegeResult.ErrorCode == 2000)
                {
                    //var privilegeId = long.Parse(privilegeResult.Content.ToString());
                    //privilegeIds.Add(privilegeId);
                    index++;
                }
            }

            //if (privilegeIds.Any())
            //{
            //    privilegeIds.Add(menuId);
            //    service.Configure(menuId, privilegeIds.ToArray());
            //}
        }

        #endregion

        #region 写入权限资源

        private List<string> GetScopeNames(List<string> resources)
        {
            var scopeNames = new List<string>();

            try
            {
                foreach (var res in resources)
                {
                    var array = res.TrimStart('/').TrimEnd('/').ToLower().Split('/');
                    if (array.Length >= 2)
                    {
                        var scopeName = string.Empty;
                        var apiArray = new string[2];
                        Array.ConstrainedCopy(array, 0, apiArray, 0, 2);

                        if (array.Length > 5)
                        {
                            var controllerArray = new string[2];
                            var actionArray = new string[array.Length - 4];

                            Array.ConstrainedCopy(array, 2, controllerArray, 0, 2);
                            Array.ConstrainedCopy(array, 4, actionArray, 0, array.Length - 4);

                            scopeName = $"{string.Join('_', apiArray.Reverse())}|{string.Join('.', controllerArray)}.{string.Join('/', actionArray)}".TrimEnd('|');
                        }
                        else
                        {
                            var pathArray = new string[array.Length - 2];
                            Array.ConstrainedCopy(array, 2, pathArray, 0, array.Length - 2);
                            scopeName = $"{string.Join('_', apiArray.Reverse())}|{string.Join('.', pathArray)}".TrimEnd('|');
                        }

                        scopeNames.Add(scopeName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} --- {JsonConvert.SerializeObject(resources)}");
            }

            return scopeNames;
        }

        #endregion
    }
}
