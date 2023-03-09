using GoldCloud.Infrastructure.Shared.Enumerations;
using GoldCloud.Infrastructure.Shared.Exception;
using GoldCloud.Permissions.Api.Base;
using GoldCloud.Permissions.Api.Dtos.Menu;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorCode = GoldCloud.Infrastructure.Shared.Enumerations.ErrorCode;
using GoldCloud.Infrastructure.Common.ValueObjects;
using GoldCloud.Infrastructure.DataBase.Constant;
using GoldCloud.Infrastructure.ApiResource.Attributes;

namespace GoldCloud.Permissions.Api.Controllers.Menu.Device
{
    /// <summary>
    /// 菜单
    /// </summary>
    //[Authorize]
    [ControllerName(ControllerNames.Menu)]
    [ApiExplorerSettings(GroupName = ApiGroupName.Device)]
    public class MenuController : DeviceApiBaseController
    {
        #region 根据角色获取菜单

        /// <summary>
        /// 根据角色获取菜单
        /// </summary>
        /// <param name="roleIds">角色标识集合</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("role/list")]
        [ProducesResponseType(typeof(List<MenuDto>), 200)]
        [MethodInfo("根据角色获取菜单", "终端设备根据角色获取菜单接口")]
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

        #region 获取菜单列表

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="dto"><see cref="GetMenuListDto" /> </param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("list")]
        [ProducesResponseType(typeof(PagedList<MenuDto>), 200)]
        [MethodInfo("获取菜单列表", "终端设备获取菜单列表接口")]
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
        [MethodInfo("获取菜单详情", "终端设备获取菜单详情接口")]
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
        [MethodInfo("获取下级菜单", "终端设备获取下级菜单接口")]
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
    }
}
