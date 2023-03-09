using GoldCloud.Domain.Interfaces.Commands.Menu;
using GoldCloud.Domain.Interfaces.Domain;
using GoldCloud.Infrastructure.Shared.Enumerations;
using GoldCloud.Infrastructure.Shared.ValueObjects;
using GoldCloud.Infrastructure.Shared.Exception;
using GoldCloud.Permissions.Api.Base;
using GoldCloud.Permissions.Api.Dtos;
using GoldCloud.Permissions.Api.Dtos.Common;
using GoldCloud.Permissions.Api.Dtos.Menu;
using GoldCloud.Permissions.Api.Extensions;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorCode = GoldCloud.Infrastructure.Shared.Enumerations.ErrorCode;
using GoldCloud.Infrastructure.Common.ValueObjects;
using GoldCloud.Infrastructure.DataBase.Constant;
using GoldCloud.Infrastructure.ApiResource.Attributes;

namespace GoldCloud.Permissions.Api.Controllers.Menu.Admin
{
    /// <summary>
    /// 菜单
    /// </summary>
    //[Authorize]
    [ControllerName(ControllerNames.Menu)]
    [ApiExplorerSettings(GroupName = ApiGroupName.Admin)]
    public class MenuController : AdminApiBaseController
    {
        #region 增删改

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="dto"><see cref="CreateMenuDto" /></param>
        [HttpPost]
        [ActionName("create")]
        [ProducesResponseType(typeof(long), 200)]
        [MethodInfo("创建菜单", "默认内部创建菜单接口")]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuDto dto)
        {
            var db = GetDataBaseDB();
            var command = Mapper.Map<CreateMenuCommand>(dto);

            var apiScopes = db.ApiScopes.Where(x => dto.ScopeNames.Contains(x.Name));
            if (apiScopes.Any())
                command.Scopes = await apiScopes.Select(x => Mapper.Map<ApiScopeEntity>(x)).ToListAsync();

            var actorId = dto.Id ?? await ClusterClient.GetUniqueIdService().NewIntegerId();
            await ClusterClient.GetGrain<IMenuGrain>(actorId).Execute(command);

            return new OkObjectResult(actorId);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="dto"><see cref="UpdateMenuDto" /></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("update")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("更新菜单", "管理后台更新菜单接口")]
        public async Task<IActionResult> UpdateMenu([FromBody] UpdateMenuDto dto)
        {
            using var db = GetDataBaseDB();

            var entity = db.Menus.FirstOrDefault(x => x.Id == dto.Id);
            if (entity is null)
                throw new GoldCloudException(ErrorCode.ObjectAlreadyExists, $"未找到 {dto.Name} 菜单信息");

            if (entity.Id == dto.ParentId)
                throw new GoldCloudException(ErrorCode.Forbidden, $"上级菜单不能为自己");

            var command = Mapper.Map<UpdateMenuCommand>(dto);
            await ClusterClient.GetGrain<IMenuGrain>(dto.Id).Execute(command);

            return new OkObjectResult(true);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="dto"><see cref="RemoveMenuDto"/></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("delete")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("删除菜单", "管理后台删除菜单接口")]
        public async Task<IActionResult> DeleteMenu([FromBody] RemoveMenuDto dto)
        {
            using var db = GetDataBaseDB();
            var entity = db.Menus.FirstOrDefault(x => x.Id == dto.Id);
            if (entity is null)
                throw new GoldCloudException(ErrorCode.ObjectAlreadyExists, "未找到菜单信息");
            if (db.Menus.Any(x => x.ParentId == dto.Id))
                throw new GoldCloudException(ErrorCode.Forbidden, "存在下级菜单,不可删除");
            if (db.MenuPermissionAssociations.Any(x => x.MenuId == dto.Id))
                throw new GoldCloudException(ErrorCode.Forbidden, "菜单已配置权限,请先删除菜单权限");

            await ClusterClient.GetGrain<IMenuGrain>(dto.Id).Execute(new DeleteMenuCommand());

            return new OkObjectResult(true);
        }

        #endregion

        #region 配置菜单权限

        /// <summary>
        /// 配置菜单权限
        /// </summary>
        /// <param name="dto"><see cref="ConfigureMenuPermissionsDto" /></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("configure")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("配置菜单权限", "管理后台配置菜单权限接口")]
        public async Task<IActionResult> ConfigurePermissions([FromBody] ConfigureMenuPermissionsDto dto)
        {
            using var db = GetDataBaseDB();
            var menu = await db.Menus.FirstOrDefaultAsync(x => x.Id == dto.MenuId && !x.Disabled);
            if (menu == null)
                throw new GoldCloudException(ErrorCode.ResourcesNotFound, "未找到菜单信息");

            var permissions = new List<PermissionInfo>();
            foreach (var permissionId in dto.PermissionIds)
            {
                var permission = await db.Permissions.FirstOrDefaultAsync(x => x.Id == permissionId);
                if (permission is null)
                    throw new GoldCloudException(ErrorCode.ResourcesNotFound, $"{permissionId} 权限信息不存在");

                if (permission.SystemId != menu.SystemId)
                    throw new GoldCloudException(ErrorCode.Forbidden, $"{permissionId} 权限归属系统与菜单归属系统不同");

                permissions.Add(Mapper.Map<PermissionInfo>(permission));
            }

            await ClusterClient.GetGrain<IMenuGrain>(dto.MenuId).Execute(new ConfigurePermissionCommand() { Permissions = permissions });

            return new OkObjectResult(true);
        }

        #endregion

        #region 不校验配置菜单权限

        /// <summary>
        /// 不校验配置菜单权限
        /// </summary>
        /// <param name="dto"><see cref="ConfigureMenuPermissionsDto" /></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("configureNoVerify")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("不校验配置菜单权限", "管理后台不校验配置菜单权限接口")]
        public async Task<IActionResult> ConfigureNoVerify([FromBody] ConfigureMenuPermissionsDto dto)
        {
            using var db = GetDataBaseDB();
            var permissions = dto.PermissionIds.Select(x => new PermissionInfo() { Id = x }).ToList();
            await ClusterClient.GetGrain<IMenuGrain>(dto.MenuId).Execute(new ConfigurePermissionCommand() { Permissions = permissions });

            return new OkObjectResult(true);
        }

        #endregion

        #region 移动菜单

        /// <summary>
        /// 移动菜单
        /// </summary>
        /// <param name="dto"><see cref="MoveMenuDto" /></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("move")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("移动菜单", "管理后台移动菜单接口")]
        public async Task<IActionResult> MoveMenu([FromBody] MoveMenuDto dto)
        {
            using var db = GetDataBaseDB();

            var entity = await db.Menus.FirstOrDefaultAsync(x => x.Id == dto.MenuId);
            if (entity is null)
                throw new GoldCloudException(ErrorCode.ResourcesNotFound, "菜单信息不存在");
            if (dto.ParentId == entity.ParentId)
                throw new GoldCloudException(ErrorCode.InvalidArgument, "菜单上级标识未变化");

            await ClusterClient.GetGrain<IMenuGrain>(entity.Id).Execute(new MoveMenuCommand() { ParentId = dto.ParentId });

            return new OkObjectResult(true);
        }

        #endregion

        #region 根据角色获取菜单

        /// <summary>
        /// 根据角色获取菜单
        /// </summary>
        /// <param name="roleIds">角色标识集合</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("role/list")]
        [ProducesResponseType(typeof(List<MenuDto>), 200)]
        [MethodInfo("根据角色获取菜单", "管理后台根据角色获取菜单接口")]
        public async Task<IActionResult> GetRoleMenus([FromQuery] string[] roleIds)
        {
            await Task.CompletedTask;
            using var db = GetDataBaseDB();

            var perIds = await db.RolePermissionAssociations.LoadWith(x => x.PermissionEntity)
                .Where(x => roleIds.Contains(x.RoleId) && x.PermissionEntity.Type == PermissionType.Menu)
                .Select(x => x.PermissionId).ToListAsync();

            var data = db.MenuPermissionAssociations.LoadWith(x => x.MenuEntity)
                .LoadWith(x => x.PermissionEntity).LoadWith(x => x.MenuEntity.System)
                .Where(x => perIds.Contains(x.PermissionId))
                .Select(x => Mapper.Map<MenuDto>(x.MenuEntity)).ToListAsync();

            return new OkObjectResult(data);
        }

        #endregion

        #region 获取系统菜单树

        /// <summary>
        /// 获取系统菜单树
        /// </summary>
        /// <param name="systemId">系统标识</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("system/tree")]
        [ProducesResponseType(typeof(List<MenuTreeBySystemDto>), 200)]
        [MethodInfo("获取系统菜单树", "管理后台获取系统菜单树接口")]
        public async Task<IActionResult> GetSystemMenuTree(long? systemId = null)
        {
            using var db = GetDataBaseDB();
            List<MenuTreeBySystemDto> data = new List<MenuTreeBySystemDto>();

            var list = await db.Menus.WhereIf(x => x.SystemId == systemId, systemId != null).OrderBy(x => x.Order).ToListAsync();
            foreach (var group in list.GroupBy(x => x.SystemId))
            {
                var system = await db.System.FirstOrDefaultAsync(x => x.Id == group.FirstOrDefault().SystemId);
                var systemInfo = new SystemInfo() { Id = system.Id, Name = system.Name, Remark = system.Remark };

                List<MenuTreeNode> tree = new List<MenuTreeNode>();
                var nodes = group.Where(x => x.ParentId == null).ToList();
                var subNodes = group.Where(x => x.ParentId != null).Select(x => Mapper.Map<MenuTreeNode>(x)).ToList();
                subNodes.ForEach(x => x.System = systemInfo);
                foreach (var item in nodes)
                {
                    var treeNode = Mapper.Map<MenuTreeNode>(item);
                    treeNode.System = systemInfo;
                    treeNode.Children.AddRange(subNodes.Recursion(item.Id));
                    tree.Add(treeNode);
                }

                data.Add(new MenuTreeBySystemDto()
                {
                    Id = systemInfo.Id.ToString(),
                    Name = systemInfo.Name,
                    Remark = systemInfo.Remark,
                    Children = tree
                });
            }

            return new OkObjectResult(data);
        }

        #endregion

        #region 获取菜单树

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("tree")]
        [ProducesResponseType(typeof(List<MenuTreeNode>), 200)]
        [MethodInfo("获取菜单树", "管理后台获取菜单树接口")]
        public async Task<IActionResult> GetTree([FromQuery] GetMenuTreeNodeDto dto)
        {
            var data = new List<MenuTreeNode>();
            using var db = GetDataBaseDB();

            if (dto.Lazy)
            {
                var query = db.Menus.LoadWith(x => x.System)
                               .Where(x => x.ParentId == dto.ParentId && !x.Disabled)
                               .Select(x => Mapper.Map<MenuTreeNode>(x));

                data.AddRange(query);
            }
            else
            {
                #region MenuCte

                var menuCte = db.GetCte<Infrastructure.DataBase.Entities.Menu>(mCte =>
                {
                    return (
                        from m in db.Menus.LoadWith(x => x.System).Where(x => !x.Disabled)
                        where m.ParentId == dto.ParentId
                        select m
                    ).Concat(
                        from m in db.Menus.LoadWith(x => x.System).Where(x => !x.Disabled)
                        from pm in mCte.InnerJoin(pm => m.ParentId == pm.Id)
                        select m
                        );
                });

                #endregion MenuCte

                var nodes = await (from m in menuCte
                                   orderby m.Order
                                   select Mapper.Map<MenuTreeNode>(m)).ToListAsync();

                data.AddRange(nodes.Recursion(dto.ParentId));
            }

            return new OkObjectResult(data);
        }

        #endregion

        #region 获取菜单树(带权限树)

        /// <summary>
        /// 获取系统菜单树/权限树
        /// </summary>
        /// <param name="dto"><see cref="GetMPTreeNodeDto" /></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("mpTree")]
        [ProducesResponseType(typeof(List<TreeNode>), 200)]
        [MethodInfo("获取菜单树(带权限树)", "管理后台获取菜单树(带权限树)接口")]
        public async Task<IActionResult> GetTreeNodeWithPermission([FromQuery] GetMPTreeNodeDto dto)
        {
            List<TreeNode> list = new List<TreeNode>();
            using (var db = GetDataBaseDB())
            {

                #region 递归加载 子级菜单及菜单对应权限列表

                #region MenuCte

                var menuCte = db.GetCte<Infrastructure.DataBase.Entities.Menu>(mCte =>
                {
                    return (
                        from m in db.Menus.Where(x => !x.Disabled)
                        where m.ParentId == dto.ParentId
                        select m
                    ).Concat(
                        from m in db.Menus.Where(x => !x.Disabled)
                        from pm in mCte.InnerJoin(pm => m.ParentId == pm.Id)
                        select m
                        );
                });

                #endregion MenuCte

                var menus = await menuCte.Where(x => !x.Disabled).LoadWith(x => x.System)
                    .WhereIf(x => x.SystemId == dto.SystemId, dto.SystemId != null)
                    .OrderBy(x => x.Order).ToListAsync();
                foreach (var item in menus.GroupBy(x => x.SystemId))
                {
                    var mData = menus.Select(m => Mapper.Map<MenuDto>(item)
                    .ToTreeNode(m.Id, m.Name, m.ParentId, TreeNodeType.Menu, false, m.Remark, Mapper.Map<SystemInfo>(item.FirstOrDefault()?.System))).ToList();
                    list.AddRange(mData.AsQueryable().Recursion(dto.ParentId, db));
                }

                #endregion
            }

            return new OkObjectResult(list);
        }

        #endregion

        #region 获取菜单列表

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="dto"><see cref="GetMenuListDto" /> </param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("list")]
        [ProducesResponseType(typeof(PagedList<MenuDto>), 200)]
        [MethodInfo("获取菜单列表", "管理后台获取菜单列表接口")]
        public async Task<IActionResult> GetList([FromQuery] GetMenuListDto dto)
        {
            PagedList<MenuDto> response = new();
            using var db = GetDataBaseDB();

            var query = db.Menus.LoadWith(x => x.System)
                .WhereIf(x => x.Name.Contains(dto.KeyWord), !string.IsNullOrWhiteSpace(dto.KeyWord))
                .WhereIf(x => x.Disabled == dto.Disabled, dto.Disabled != null)
                .OrderBy(x => x.Order)
                .Select(x => Mapper.Map<MenuDto>(x));

            int count = await query.CountAsync();
            var data = await query.PageBy(dto).ToListAsync();

            response.Data.AddRange(data);
            response.TotalCount = count;
            response.PageSize = dto.PageSize;

            return new OkObjectResult(response);
        }

        #endregion

        #region 获取菜单详情

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="id">菜单标识</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("detail")]
        [ProducesResponseType(typeof(MenuDto), 200)]
        [MethodInfo("获取菜单详情", "管理后台获取菜单详情接口")]
        public async Task<IActionResult> GetById([FromQuery] long id)
        {
            using var db = GetDataBaseDB();

            var entity = await db.Menus.LoadWith(x => x.System).FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                throw new GoldCloudException(ErrorCode.ResourcesNotFound, "未找到对应菜单信息");

            var response = Mapper.Map<MenuDto>(entity);

            return new OkObjectResult(response);
        }

        #endregion

        #region 获取下级菜单

        /// <summary>
        /// 获取下级菜单
        /// </summary>
        /// <param name="pId">上级菜单标识</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("children")]
        [ProducesResponseType(typeof(List<MenuDto>), 200)]
        [MethodInfo("获取下级菜单", "管理后台获取下级菜单接口")]
        public async Task<IActionResult> GetListByParent([FromQuery] long? pId)
        {
            using var db = GetDataBaseDB();

            var data = await db.Menus.LoadWith(x => x.System)
                .Where(x => x.ParentId == pId && !x.Disabled)
                .OrderBy(x => x.Order)
                .Select(x => Mapper.Map<MenuDto>(x))
                .ToListAsync();


            return new OkObjectResult(data);
        }

        #endregion

        #region 禁用菜单

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="ids">菜单标识</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("disable")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("禁用菜单", "管理后台禁用菜单接口")]
        public async Task<IActionResult> DisableMenu([FromBody] params long[] ids)
        {
            var grains = new List<IMenuGrain>();
            foreach (var id in ids)
            {
                var grain = ClusterClient.GetGrain<IMenuGrain>(id);
                if (grain is null)
                    throw new GoldCloudException(ErrorCode.ResourcesNotFound, "菜单不存在");
                grains.Add(grain);
            }

            foreach (var grain in grains)
                await grain.Execute(new DisableMenuCommand());

            return new OkObjectResult(true);
        }

        #endregion
    }
}
