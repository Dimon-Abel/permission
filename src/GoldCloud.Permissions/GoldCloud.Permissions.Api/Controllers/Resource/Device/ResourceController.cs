using GoldCloud.Infrastructure.ApiResource.Attributes;
using GoldCloud.Infrastructure.Common.ValueObjects;
using GoldCloud.Infrastructure.DataBase.Constant;
using GoldCloud.Permissions.Api.Base;
using GoldCloud.Permissions.Api.Dtos;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GoldCloud.Permissions.Api.Controllers.Resource.Device
{
    /// <summary>
    /// 资源
    /// </summary>
    [ControllerName(ControllerNames.Resource)]
    [ApiExplorerSettings(GroupName = ApiGroupName.Device)]
    public class ResourceController : DeviceApiBaseController
    {
        #region Api Resource 分页查询

        /// <summary>
        /// Api Resource 分页查询
        /// </summary>
        /// <param name="dto"><see cref="ApiResourceDto" /></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ApiResource/List")]
        [ProducesResponseType(typeof(PagedList<ApiResourceDto>), 200)]
        [MethodInfo("Api资源分页查询", "终端设备Api资源分页查询接口")]
        public async Task<IActionResult> GetApiResourceList([FromQuery] GetResourceListDto dto)
        {
            PagedList<ApiResourceDto> response = new();
            using var db = GetDataBaseDB();

            var query = db.ApiResources.WhereIf(x => x.Name.Contains(dto.Name), !string.IsNullOrWhiteSpace(dto.Name))
                .WhereIf(x => x.DisplayName.Contains(dto.DisplayName), !string.IsNullOrWhiteSpace(dto.DisplayName))
                .WhereIf(x => x.Name.Contains(dto.KeyWord) || x.DisplayName.Contains(dto.KeyWord), !string.IsNullOrWhiteSpace(dto.KeyWord));

            var count = await query.CountAsync();
            var list = await query.PageBy(dto)
                .Select(x => Mapper.Map<ApiResourceDto>(x)).ToListAsync();

            response.Data.AddRange(list);
            response.TotalCount = count;
            response.PageSize = dto.PageSize;

            return new OkObjectResult(response);
        }

        #endregion

        #region Api Scope 分页查询

        /// <summary>
        /// Api Scope 分页查询
        /// </summary>
        /// <param name="dto"><see cref="ApiScopeDto" /></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("ApiScope/List")]
        [ProducesResponseType(typeof(PagedList<ApiScopeDto>), 200)]
        [MethodInfo("ApiScope分页查询", "终端设备ApiScope分页查询接口")]
        public async Task<IActionResult> GetScopeList([FromQuery] GetScopeListDto dto)
        {
            PagedList<ApiScopeDto> response = new();
            using var db = GetDataBaseDB();

            var query = db.ApiScopes.LoadWith(x => x.ApiResourceEntity)
                .WhereIf(x => x.Id == dto.Id.Value, dto.Id.HasValue)
                .WhereIf(x => x.Name.Contains(dto.Name), !string.IsNullOrWhiteSpace(dto.Name))
                .WhereIf(x => x.DisplayName.Contains(dto.DisplayName), !string.IsNullOrWhiteSpace(dto.DisplayName))
                .WhereIf(x => x.Name.Contains(dto.KeyWord) || x.DisplayName.Contains(dto.KeyWord), !string.IsNullOrWhiteSpace(dto.KeyWord))
                .WhereIf(x => x.ApiResourceId == dto.ApiResourceId.Value, dto.ApiResourceId.HasValue);

            var count = await query.CountAsync();
            var list = await query.PageBy(dto)
                .Select(x => Mapper.Map<ApiScopeDto>(x)).ToListAsync();

            response.Data.AddRange(list);
            response.TotalCount = count;
            response.PageSize = dto.PageSize;

            return new OkObjectResult(response);
        }

        #endregion
    }
}
