using GoldCloud.Permissions.Api.Base;
using GoldCloud.Permissions.Api.Dtos;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoldCloud.Infrastructure.Common.ValueObjects;
using GoldCloud.Infrastructure.DataBase.Constant;
using GoldCloud.Infrastructure.ApiResource.Attributes;

namespace GoldCloud.Permissions.Api.Controllers.System.Device
{
    /// <summary>
    /// 系统
    /// </summary>
    [ControllerName(ControllerNames.System)]
    [ApiExplorerSettings(GroupName = ApiGroupName.Device)]
    public class SystemController : DeviceApiBaseController
    {
        #region 获取系统详细信息

        /// <summary>
        /// 获取系统详情
        /// </summary>
        /// <param name="id">系统标识</param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("detail")]
        [ProducesResponseType(typeof(SystemDto), 200)]
        [MethodInfo("获取系统详细信息", "终端设备获取系统详细信息接口")]
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
        [MethodInfo("获取系统列表", "终端设备获取系统列表接口")]
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
        [MethodInfo("获取所有系统信息", "终端设备获取所有系统信息接口")]
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
