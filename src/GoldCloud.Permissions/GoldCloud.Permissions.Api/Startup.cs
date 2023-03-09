using System;
using GoldCloud.Infrastructure.DataBase.Extensions;
using GoldCloud.Infrastructure.Swagger;
using GoldCloud.Permissions.Api.Extensions;
using GoldCloud.Permissions.Api.Handlers;
using GoldCloud.Presentation.WebBase.Extensions;
using GoldCloud.Presentation.WebBase.Handlers;
using GoldCloud.Presentation.WebBase.Startups;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoldCloud.Permissions.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup : DefaultStartup
    {
        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="configuration">      </param>
        /// <param name="webHostEnvironment"> </param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) : base(configuration, webHostEnvironment) { }

        #endregion 初始化

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"> </param> 
        public override void ConfigureServices(IServiceCollection services)
        {
            // 基础配置
            base.ConfigureServices(services);

            // 注册应用服务
            services
                //.AddClusterClientHandler<ApiClusterClientHandler>()
                .AddVersionedApiExplorer()                                  // 注册 API Version 解释器
                .AddDistributedCacheService()                               // 注册 分布式缓存服务
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())     // 注册 AutoMappers
                .AddGoldCloudHttpClient(Configuration)                      // 注册 httpClient 服务
                .AddGoldCloudDataBase(Configuration)                        // 注册 数据库 服务
                .AddGoldCloudOptions(Configuration)                         // 注册 系统配置
                .AddPermissionHealthChecks()                                // 注册 HealthCheck
                .AddClusterClientHandler<ApiClusterClientHandler>()
                .AddConsulClustering(Configuration);


            #region 初始化系统种子数据

            //services.InitializeSystem();

            #endregion
        }

        #region 中间件、管道配置

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"> </param>
        /// <param name="env"> </param>
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            NLog.LogManager.LoadConfiguration("nlogsettings.config");

            base.Configure(app, env);

            app.UseKingMetal();

            app.UseEndpoints(endpoints => endpoints.MapHealthChecks("/health"));
        }

        #endregion
    }
}
