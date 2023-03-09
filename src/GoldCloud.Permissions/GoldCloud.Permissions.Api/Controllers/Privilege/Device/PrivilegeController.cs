using GoldCloud.Infrastructure.Shared.ValueObjects;
using GoldCloud.Infrastructure.Shared.Exception;
using GoldCloud.Infrastructure.DataBase.Entities;
using GoldCloud.Permissions.Api.Base;
using GoldCloud.Permissions.Api.Dtos.Common;
using GoldCloud.Permissions.Api.Dtos.Permission;
using GoldCloud.Permissions.Api.Extensions;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorCode = GoldCloud.Infrastructure.Shared.Enumerations.ErrorCode;
using GoldCloud.Infrastructure.Common.ValueObjects;
using GoldCloud.Infrastructure.DataBase.Constant;
using GoldCloud.Infrastructure.ApiResource.Attributes;

namespace GoldCloud.Permissions.Api.Controllers.Privilege.Device
{
    /// <summary>
    /// 权限
    /// </summary>
    [ControllerName(ControllerNames.Permission)]
    [ApiExplorerSettings(GroupName = ApiGroupName.Device)]
    public class PrivilegeController : DeviceApiBaseController
    {
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

        #region 获取所属系统权限树

        /// <summary>
        /// 获取所属系统权限树
        /// </summary>
        /// <param name="dto"><see cref="GetPermissionTreeByGroupDto" /></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("system/tree")]
        [ProducesResponseType(typeof(List<PermissionTreeBySystemDto>), 200)]
        [MethodInfo("获取所属系统权限树", "终端设备获取所属系统权限树接口")]
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
        [MethodInfo("获取权限树", "终端设备获取权限树接口")]
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
        [MethodInfo("获取权限列表", "终端设备获取权限列表接口")]
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
        [MethodInfo("获取权限详情", "终端设备获取权限详情接口")]
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
        [MethodInfo("获取下级权限", "终端设备获取下级权限接口")]
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
    }
}
