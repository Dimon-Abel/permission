using GoldCloud.Domain.Interfaces.Commands.Permission;
using GoldCloud.Domain.Interfaces.Domain;
using GoldCloud.Infrastructure.Shared.ValueObjects;
using GoldCloud.Infrastructure.Shared.Exception;
using GoldCloud.Infrastructure.DataBase.Entities;
using GoldCloud.Permissions.Api.Base;
using GoldCloud.Permissions.Api.Dtos;
using GoldCloud.Permissions.Api.Dtos.Common;
using GoldCloud.Permissions.Api.Dtos.Permission;
using GoldCloud.Permissions.Api.Extensions;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorCode = GoldCloud.Infrastructure.Shared.Enumerations.ErrorCode;
using GoldCloud.Infrastructure.Common.ValueObjects;
using GoldCloud.Infrastructure.ApiResource.Attributes;

namespace GoldCloud.Permissions.Api.Controllers
{
    /// <summary>
    /// 权限
    /// </summary>
    //[Authorize]
    [ApiController]
    public class PrivilegeController : ApiBaseController
    {
        #region 增删改

        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="dto"><see cref="CreatePermissionDto" /></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("create")]
        [ProducesResponseType(typeof(long), 200)]
        [MethodInfo("创建权限", "默认内部创建权限接口")]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionDto dto)
        {
            var privilegeId = dto.Id ?? await ClusterClient.GetUniqueIdService().NewIntegerId();
            using (var db = GetDataBaseDB())
            {
                if (dto.ParentId != null && !db.Permissions.Any(x => x.Id == dto.ParentId))
                    throw new GoldCloudException(ErrorCode.InvalidArgument, "上级权限不存在");

                if (!db.System.Any(x => x.Id == dto.SystemId))
                    throw new GoldCloudException(ErrorCode.InvalidArgument, "所属系统不存在");

                var command = Mapper.Map<CreatePermissionCommand>(dto);

                if (dto.ScopeNames.Any())
                {
                    command.Resource = await db.ApiScopes.Where(x => dto.ScopeNames.Contains(x.Name))
                    .Select(x => Mapper.Map<ApiScopeEntity>(x))
                    .ToListAsync();
                }

                await ClusterClient.GetGrain<IPermissionGrain>(privilegeId).Execute(command);
            }

            return new OkObjectResult(privilegeId);
        }

        /// <summary>
        /// 不校验创建权限
        /// </summary>
        /// <param name="dto"><see cref="CreatePermissionDto" /></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("createNoVerify")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("不校验创建权限", "默认内部不校验创建权限接口")]
        public async Task<IActionResult> CreatePermissionNoVerify([FromBody] CreatePermissionDto dto)
        {
            var privilegeId = dto.Id ?? await ClusterClient.GetUniqueIdService().NewIntegerId();

            using (var db = GetDataBaseDB())
            {
                var command = Mapper.Map<CreatePermissionCommand>(dto);

                if (dto.ScopeNames.Any())
                {
                    command.Resource = await db.ApiScopes.Where(x => dto.ScopeNames.Contains(x.Name))
                    .Select(x => Mapper.Map<ApiScopeEntity>(x))
                    .ToListAsync();
                }

                await ClusterClient.GetGrain<IPermissionGrain>(await ClusterClient.GetUniqueIdService().NewIntegerId()).Execute(command);
            }

            return new OkObjectResult(privilegeId);
        }

        /// <summary>
        /// 更新权限
        /// </summary>
        /// <param name="dto"><see cref="UpdatePermissionDto" /></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("update")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("更新权限", "默认内部更新权限接口")]
        public async Task<IActionResult> UpdatePermission([FromBody] UpdatePermissionDto dto)
        {
            using (var db = GetDataBaseDB())
            {
                var entity = db.Permissions.FirstOrDefault(x => x.Id == dto.Id.Value);
                if (entity is null)
                    throw new GoldCloudException(ErrorCode.ObjectAlreadyExists, $"未找到权限信息");

                if (dto.ParentId != null && !db.Permissions.Any(x => x.Id == dto.ParentId))
                    throw new GoldCloudException(ErrorCode.InvalidArgument, "上级权限不存在");

                if (entity.Id == dto.ParentId)
                    throw new GoldCloudException(ErrorCode.Forbidden, $"上级权限不能为自己");

                if (!db.System.Any(x => x.Id == dto.SystemId))
                    throw new GoldCloudException(ErrorCode.InvalidArgument, "所属系统不存在");

                var command = Mapper.Map<UpdatePermissionCommand>(dto);

                if (dto.Resource.Any())
                {
                    command.Resource = await db.ApiScopes.Where(x => dto.Resource.Contains(x.Id))
                    .Select(x => Mapper.Map<ApiScopeEntity>(x))
                    .ToListAsync();
                }

                await ClusterClient.GetGrain<IPermissionGrain>(dto.Id.Value).Execute(command);
            }

            return new OkObjectResult(true);
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="dtos"><see cref="RemovePermissionDto"/></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("delete")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("删除权限", "默认内部删除权限接口")]
        public async Task<IActionResult> DeletePermission([FromBody] params RemovePermissionDto[] dtos)
        {
            using var db = GetDataBaseDB();
            foreach (var dto in dtos)
            {
                var entity = db.Permissions.FirstOrDefault(x => x.Id == dto.Id);
                if (entity is null)
                    throw new GoldCloudException(ErrorCode.ObjectAlreadyExists, "未找到权限信息");
                if (entity.IsSystem)
                    throw new GoldCloudException(ErrorCode.ObjectAlreadyExists, "系统级权限不可删除");
                if (db.Permissions.Any(x => x.ParentId == dto.Id))
                    throw new GoldCloudException(ErrorCode.Forbidden, "存在下级权限,不可删除");
            }

            foreach (var dto in dtos)
                await ClusterClient.GetGrain<IPermissionGrain>(dto.Id).Execute(new DeletePermissionCommand() { });

            return new OkObjectResult(true);
        }

        #endregion

        #region 配置角色权限

        /// <summary>
        /// 配置角色权限
        /// </summary>
        /// <param name="dto"><see cref="ConfigureRolePermissionDto" /></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("configure")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("配置角色权限", "默认内部配置角色权限接口")]
        public async Task<IActionResult> ConfigurePermissions([FromBody] ConfigureRolePermissionDto dto)
        {
            using var db = GetDataBaseDB();
            var id = await ClusterClient.GetUniqueIdService().NewIntegerId();

            if (string.IsNullOrWhiteSpace(dto.RoleId))
                throw new GoldCloudException(ErrorCode.InvalidArgument, "角色标识不能为空");

            if (dto.PermissionIds is null)
                throw new GoldCloudException(ErrorCode.InvalidArgument, "权限标识集合不能为空");

            foreach (var permissionId in dto.PermissionIds)
            {
                var permission = await db.Permissions.FirstOrDefaultAsync(x => x.Id == permissionId);
                if (permission is null)
                    throw new GoldCloudException(ErrorCode.ResourcesNotFound, $"{permissionId} 权限信息不存在");
            }

            await ClusterClient.GetGrain<IPermissionGrain>(id).Execute(new ConfigureRolePermissionCommand() { RoleId = dto.RoleId, PermissionIds = dto.PermissionIds });
            return new OkObjectResult(true);
        }

        #endregion

        #region 移动权限

        /// <summary>
        /// 移动权限
        /// </summary>
        /// <param name="dto"><see cref="MovePermissionDto" /></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("move")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("移动权限", "默认内部移动权限接口")]
        public async Task<IActionResult> MovePermission([FromBody] MovePermissionDto dto)
        {
            using var db = GetDataBaseDB();

            var entity = await db.Permissions.FirstOrDefaultAsync(x => x.Id == dto.PermissionId);
            if (entity is null)
                throw new GoldCloudException(ErrorCode.ResourcesNotFound, "权限信息不存在");
            if (dto.ParentId == entity.ParentId)
                throw new GoldCloudException(ErrorCode.InvalidArgument, "权限上级标识未变化");

            await ClusterClient.GetGrain<IMenuGrain>(entity.Id).Execute(new MovePermissionCommand() { ParentId = dto.ParentId });
            return new OkObjectResult(true);
        }

        #endregion

        #region 根据角色获取权限信息

        /// <summary>
        /// 根据角色获取权限信息
        /// </summary>
        /// <param name="roleIds">角色标识集合</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName("role/permission")]
        [ProducesResponseType(typeof(List<PermissionDto>), 200)]
        [MethodInfo("根据角色获取权限信息", "默认内部根据角色获取权限信息接口")]
        public async Task<IActionResult> GetPermissionByRole([FromQuery] params string[] roleIds)
        {
            using var db = GetDataBaseDB();

            var query = db.RolePermissionAssociations.LoadWith(x => x.PermissionEntity)
                .LoadWith(x => x.PermissionEntity.System).Where(x => roleIds.Contains(x.RoleId));

            var data = await query.Select(x => Mapper.Map<PermissionDto>(x.PermissionEntity)).ToListAsync();

            return new OkObjectResult(data);
        }

        #endregion

        #region 根据菜单获取权限

        /// <summary>
        /// 根据菜单获取权限信息
        /// </summary>
        /// <param name="menuIds">菜单标识集合</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ActionName("menu/permission")]
        [ProducesResponseType(typeof(List<PermissionDto>), 200)]
        [MethodInfo("根据菜单获取权限", "默认内部根据菜单获取权限接口")]
        public async Task<IActionResult> GetPermissionByMenu([FromQuery] params long[] menuIds)
        {
            using var db = GetDataBaseDB();
            var data = await db.MenuPermissionAssociations.LoadWith(x => x.PermissionEntity)
                .Where(x => menuIds.ToList().Contains(x.MenuId))
                .Select(x => Mapper.Map<PermissionDto>(x.PermissionEntity))
                .ToListAsync();

            return new OkObjectResult(data);
        }

        #endregion

        #region 删除角色权限

        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="dto"><see cref="RemoveRolePermissionDto"/></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("role/delete")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("删除角色权限", "默认内部删除角色权限接口")]
        public async Task<IActionResult> DeleteRolePermission([FromBody] RemoveRolePermissionDto dto)
        {
            var id = await ClusterClient.GetUniqueIdService().NewIntegerId();
            await ClusterClient.GetGrain<IPermissionGrain>(id).Execute(new DeleteRolePermissionCommand() { RoleId = dto.RoleId });
            return new OkObjectResult(true);
        }

        #endregion

        #region 获取所属系统权限树

        /// <summary>
        /// 获取所属系统权限树
        /// </summary>
        /// <param name="dto"><see cref="GetPermissionTreeByGroupDto" /></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("system/tree")]
        [ProducesResponseType(typeof(List<PermissionTreeBySystemDto>), 200)]
        [MethodInfo("获取所属系统权限树", "默认内部获取所属系统权限树接口")]
        public async Task<IActionResult> GetPermissionTreeByGroup([FromQuery] GetPermissionTreeByGroupDto dto)
        {
            using var db = GetDataBaseDB();
            List<PermissionTreeBySystemDto> data = new List<PermissionTreeBySystemDto>();

            var list = await db.Permissions.WhereIf(x => x.SystemId == dto.SystemId, dto.SystemId != null)
                .WhereIf(x => x.Name.Contains(dto.KeyWord), !string.IsNullOrEmpty(dto.KeyWord)).OrderBy(x => x.Order).ToListAsync();
            foreach (var group in list.GroupBy(x => x.SystemId))
            {
                var system = await db.System.FirstOrDefaultAsync(x => x.Id == group.FirstOrDefault().SystemId);
                var systemInfo = new SystemInfo() { Id = system.Id, Name = system.Name, Remark = system.Remark };

                List<PermissionTreeNode> tree = new List<PermissionTreeNode>();
                if (!string.IsNullOrWhiteSpace(dto.KeyWord))
                {
                    var nodes = group.Where(x => x.ParentId == null).ToList();
                    var subNodes = group.Where(x => x.ParentId != null).Select(x => Mapper.Map<PermissionTreeNode>(x)).ToList();
                    subNodes.ForEach(x => x.System = systemInfo);
                    foreach (var item in nodes)
                    {
                        var treeNode = Mapper.Map<PermissionTreeNode>(item);
                        treeNode.System = systemInfo;
                        treeNode.Children.AddRange(subNodes.Recursion(item.Id));
                        tree.Add(treeNode);
                    }
                }
                else
                {
                    var nodes = group.ToList();
                    foreach (var item in nodes)
                    {
                        var treeNode = Mapper.Map<PermissionTreeNode>(item);
                        treeNode.System = systemInfo;
                        tree.Add(treeNode);
                    }
                }

                data.Add(new PermissionTreeBySystemDto()
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

        #region 获取权限树

        /// <summary>
        /// 获取权限树
        /// </summary>
        /// <param name="dto"><see cref="GetPermissionTreeNodeDto" /></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("tree")]
        [ProducesResponseType(typeof(List<PermissionTreeNode>), 200)]
        [MethodInfo("获取权限树", "默认内部获取权限树接口")]
        public async Task<IActionResult> GetTree([FromQuery] GetPermissionTreeNodeDto dto)
        {
            var data = new List<PermissionTreeNode>();

            using (var db = GetDataBaseDB())
            {
                var query = db.Permissions.LoadWith(x => x.System);

                if (dto.Lazy)
                {
                    var list = await query.Where(x => x.ParentId == dto.ParentId)
                        .Select(x => Mapper.Map<PermissionTreeNode>(x))
                        .ToListAsync();

                    data.AddRange(list);
                }
                else
                {
                    var permissionIds = await query.Select(x => x.Id).ToListAsync();
                    var permissionCte = db.GetCte<Permission>(mCte =>
                    {
                        return (
                            from p in db.Permissions
                            where permissionIds.Contains(p.ParentId.Value)
                            select p
                        ).Concat(
                            from p in db.Permissions
                            from pm in mCte.InnerJoin(pm => p.ParentId == pm.Id)
                            select p
                            );
                    });

                    var nodes = await (from m in permissionCte.LoadWith(x => x.System)
                                       select Mapper.Map<PermissionTreeNode>(m)).ToListAsync();
                    foreach (var item in query)
                    {
                        var treeNode = Mapper.Map<PermissionTreeNode>(item);
                        treeNode.Children.AddRange(nodes.Recursion(item.Id));
                        data.Add(treeNode);
                    }
                }
            }

            return new OkObjectResult(data);
        }

        #endregion

        #region 获取权限列表

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="dto"><see cref="GetPermissionListDto" /></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("list")]
        [ProducesResponseType(typeof(PagedList<PermissionDto>), 200)]
        [MethodInfo("获取权限列表", "默认内部获取权限列表接口")]
        public async Task<IActionResult> GetList([FromQuery] GetPermissionListDto dto)
        {
            PagedList<PermissionDto> response = new();
            using var db = GetDataBaseDB();

            var query = db.Permissions.LoadWith(x => x.System)
                .WhereIf(x => x.Name.Contains(dto.KeyWord), !string.IsNullOrWhiteSpace(dto.KeyWord))
                .WhereIf(x => x.IsSystem == dto.IsSystem.Value, dto.IsSystem != null)
                .WhereIf(x => x.Type == dto.Type.Value, dto.Type != null);

            var count = await query.CountAsync();
            var list = await query.OrderBy(x => x.Order).PageBy(dto)
                .Select(x => Mapper.Map<PermissionDto>(x)).ToListAsync();

            response.Data.AddRange(list);
            response.TotalCount = count;
            response.PageSize = dto.PageSize;

            return new OkObjectResult(response);
        }

        #endregion

        #region 获取权限详情

        /// <summary>
        /// 获取权限详情
        /// </summary>
        /// <param name="id">权限标识</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("detail")]
        [ProducesResponseType(typeof(PermissionDto), 200)]
        [MethodInfo("获取权限详情", "默认内部获取权限详情接口")]
        public async Task<IActionResult> GetById([FromQuery] long id)
        {
            using var db = GetDataBaseDB();

            var entity = await db.Permissions.LoadWith(x => x.System).FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
                throw new GoldCloudException(ErrorCode.ResourcesNotFound, "未找到对应权限信息");

            var response = Mapper.Map<PermissionDto>(entity);

            return new OkObjectResult(response);
        }

        #endregion

        #region 获取下级权限

        /// <summary>
        /// 获取下级权限
        /// </summary>
        /// <param name="pId">上级权限标识</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("children")]
        [ProducesResponseType(typeof(List<PermissionDto>), 200)]
        [MethodInfo("获取下级权限", "默认内部获取下级权限接口")]
        public async Task<IActionResult> GetListByParent([FromQuery] long? pId)
        {
            using var db = GetDataBaseDB();

            var data = await db.Permissions.LoadWith(x => x.System)
                .Where(x => x.ParentId == pId)
                .OrderBy(x => x.Order)
                .Select(x => Mapper.Map<PermissionDto>(x))
                .ToListAsync();

            return new OkObjectResult(data);
        }

        #endregion

        #region 配置权限资源

        /// <summary>
        /// 配置权限资源
        /// </summary>
        /// <param name="dto"><see cref="ConfigureResourceDto"/></param>
        /// <returns></returns>
        /// <exception cref="GoldCloudException"></exception>
        [HttpPost]
        [ActionName("configure/resource")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("配置权限资源", "默认内部配置权限资源接口")]
        public async Task<IActionResult> ConfigureResource([FromBody] ConfigureResourceDto dto)
        {
            using (var db = GetDataBaseDB())
            {
                var privilege = await db.Permissions.FirstOrDefaultAsync(x => x.Id == dto.PrivilegeId);
                if (privilege is null)
                    throw new GoldCloudException(ErrorCode.ResourcesNotFound, "未找到对应权限资源");

                var apiScopes = db.ApiScopes.Where(x => dto.ScopeNames.Contains(x.Name));

                if (apiScopes.Any())
                {
                    var apiScopeEntities = await apiScopes.Select(x => Mapper.Map<ApiScopeEntity>(x)).ToListAsync();
                    await ClusterClient.GetGrain<IPermissionGrain>(privilege.Id).Execute(new UpdateResourceCommand()
                    {
                        Resource = apiScopeEntities
                    });
                }
            }

            return new OkObjectResult(true);
        }

        #endregion
    }
}
