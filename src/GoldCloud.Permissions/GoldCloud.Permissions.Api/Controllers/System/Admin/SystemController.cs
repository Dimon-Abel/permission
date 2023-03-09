using GoldCloud.Domain.Interfaces.Commands.System;
using GoldCloud.Domain.Interfaces.Domain;
using GoldCloud.Infrastructure.Shared.Exception;
using GoldCloud.Permissions.Api.Base;
using GoldCloud.Permissions.Api.Dtos;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorCode = GoldCloud.Infrastructure.Shared.Enumerations.ErrorCode;
using GoldCloud.Infrastructure.Common.ValueObjects;
using GoldCloud.Infrastructure.DataBase.Constant;
using GoldCloud.Infrastructure.ApiResource.Attributes;

namespace GoldCloud.Permissions.Api.Controllers.System.Admin
{
    /// <summary>
    /// 系统
    /// </summary>
    [ControllerName(ControllerNames.System)]
    [ApiExplorerSettings(GroupName = ApiGroupName.Admin)]
    public class SystemController : AdminApiBaseController
    {
        #region 增删改

        /// <summary>
        /// 创建系统
        /// </summary>
        /// <param name="dto"><see cref="CreateSystemDto" /></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("create")]
        [ProducesResponseType(typeof(long), 200)]
        [MethodInfo("创建系统", "默认内部创建系统接口")]
        public async Task<IActionResult> CreateSystem([FromBody] CreateSystemDto dto)
        {
            var systemId = dto.Id ?? await ClusterClient.GetUniqueIdService().NewIntegerId();
            var command = Mapper.Map<CreateSystemCommand>(dto);
            await ClusterClient.GetGrain<ISystemGrain>(systemId).Execute(command);

            return new OkObjectResult(systemId);
        }

        /// <summary>
        /// 更新系统
        /// </summary>
        /// <param name="dto"><see cref="UpdateSystemDto" /></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("update")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("更新系统", "管理后台更新系统接口")]
        public async Task<IActionResult> UpdateSystem([FromBody] UpdateSystemDto dto)
        {
            using var db = GetDataBaseDB();

            var entity = db.System.FirstOrDefault(x => x.Id == dto.Id);
            if (entity is null)
                throw new GoldCloudException(ErrorCode.ObjectAlreadyExists, $"未找到系统信息");
            var command = Mapper.Map<UpdateSystemCommand>(dto);
            await ClusterClient.GetGrain<ISystemGrain>(dto.Id).Execute(command);

            return new OkObjectResult(true);
        }

        /// <summary>
        /// 删除系统
        /// </summary>
        /// <param name="dto"><see cref="RemoveSystemDto"/></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("delete")]
        [ProducesResponseType(typeof(bool), 200)]
        [MethodInfo("删除系统", "管理后台删除系统接口")]
        public async Task<IActionResult> DeleteSystem([FromBody] RemoveSystemDto dto)
        {
            using var db = GetDataBaseDB();
            var entity = db.System.FirstOrDefault(x => x.Id == dto.Id);
            if (entity is null)
                throw new GoldCloudException(ErrorCode.ObjectAlreadyExists, "未找到系统信息");
            if (db.Permissions.Any(x => x.SystemId == entity.Id))
                throw new GoldCloudException(ErrorCode.Forbidden, "此系统存在权限不可删除");

            await ClusterClient.GetGrain<ISystemGrain>(dto.Id).Execute(new DeleteSystemCommand() { });

            return new OkObjectResult(true);
        }

        #endregion

        #region 获取系统详细信息

        /// <summary>
        /// 获取系统详情
        /// </summary>
        /// <param name="id">系统标识</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("detail")]
        [ProducesResponseType(typeof(SystemDto), 200)]
        [MethodInfo("获取系统详细信息", "管理后台获取系统详细信息接口")]
        public async Task<IActionResult> GetById([FromQuery] long id)
        {
            using var db = GetDataBaseDB();
            var data = await db.System.FirstOrDefaultAsync(x => x.Id == id);
            return new OkObjectResult(data);
        }

        #endregion

        #region 获取系统列表

        /// <summary>
        /// 获取系统列表
        /// </summary>
        /// <param name="dto"><see cref="GetSystemListDto" /></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("list")]
        [ProducesResponseType(typeof(PagedList<SystemDto>), 200)]
        [MethodInfo("获取系统列表", "管理后台获取系统列表接口")]
        public async Task<IActionResult> GetList([FromQuery] GetSystemListDto dto)
        {
            PagedList<SystemDto> response = new();
            using var db = GetDataBaseDB();

            var query = db.System.WhereIf(x => x.Name.Contains(dto.KeyWord), !string.IsNullOrWhiteSpace(dto.KeyWord));

            var count = await query.CountAsync();
            var list = await query.OrderBy(x => x.Order).PageBy(dto).ToListAsync();
            var data = Mapper.Map<List<SystemDto>>(list);

            response.Data.AddRange(data);
            response.TotalCount = count;
            response.PageSize = dto.PageSize;

            return new OkObjectResult(response);
        }

        #endregion

        #region 获取所有系统信息

        /// <summary>
        /// 获取所有系统信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("all")]
        [ProducesResponseType(typeof(List<SystemDto>), 200)]
        [MethodInfo("获取所有系统信息", "管理后台获取所有系统信息接口")]
        public async Task<IActionResult> GetAll()
        {
            using var db = GetDataBaseDB();

            var list = await db.System.OrderBy(x => x.Order).ToListAsync();
            var data = Mapper.Map<List<SystemDto>>(list);

            return new OkObjectResult(data);
        }

        #endregion
    }
}
