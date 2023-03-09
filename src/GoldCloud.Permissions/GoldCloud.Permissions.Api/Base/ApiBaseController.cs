using AutoMapper;
using GoldCloud.Infrastructure.DataBase;
using GoldCloud.Infrastructure.DataBase.Extensions;
using GoldCloud.Presentation.WebBase.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace GoldCloud.Permissions.Api.Base
{
    /// <summary>
    /// 接口控制器基类
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApiBaseController : BaseApiController
    {
    }

    #region 管理后台

    /// <summary>
    /// 【管理后台】接口基类控制器
    /// </summary>
    [Authorize]
    [Route("Admin/[controller]/[action]")]
    public class AdminApiBaseController : BaseApiController
    {
    }

    #endregion

    #region 商户后台

    /// <summary>
    /// 【管理后台】接口基类控制器
    /// </summary>
    [Authorize]
    [Route("Mch/[controller]/[action]")]
    public class MchApiBaseController : BaseApiController
    {
    }

    #endregion

    #region 终端

    /// <summary>
    /// 【终端】接口基类控制器
    /// </summary>
    [Authorize]
    [Route("Device/[controller]/[action]")]
    public class DeviceApiBaseController : BaseApiController
    {
    }

    #endregion

    #region 移动端

    /// <summary>
    /// 【移动端】接口基类控制器
    /// </summary>
    [Authorize]
    [Route("Mobile/[controller]/[action]")]
    public class MobileApiBaseController : BaseApiController
    {
    }

    #endregion

    #region 开放

    /// <summary>
    /// 【开放】接口基类控制器
    /// </summary>
    [AllowAnonymous]
    [Route("Open/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "open")]
    public class OpenApiBaseController : BaseApiController
    {
    }

    #endregion

    #region API 接口基类

    /// <summary>
    /// API 接口基类
    /// </summary>
    [ApiController]
    public abstract class BaseApiController : ApiBaseV1Controller
    {
        #region 属性

        /// <summary>
        /// IMapper
        /// </summary>
        protected IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();

        #endregion 属性

        #region 获取数据库对象

        /// <summary>
        /// 获取数据库对象
        /// </summary>
        /// <returns> </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected GoldPermissionDB GetDataBaseDB() => ServiceProvider.GetGoldPermissionDB();

        #endregion 获取数据库对象
    }

    #endregion
}
