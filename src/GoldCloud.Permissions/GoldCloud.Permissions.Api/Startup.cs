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
        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="configuration">      </param>
        /// <param name="webHostEnvironment"> </param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) : base(configuration, webHostEnvironment) { }

        #endregion ��ʼ��

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"> </param> 
        public override void ConfigureServices(IServiceCollection services)
        {
            // ��������
            base.ConfigureServices(services);

            // ע��Ӧ�÷���
            services
                //.AddClusterClientHandler<ApiClusterClientHandler>()
                .AddVersionedApiExplorer()                                  // ע�� API Version ������
                .AddDistributedCacheService()                               // ע�� �ֲ�ʽ�������
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())     // ע�� AutoMappers
                .AddGoldCloudHttpClient(Configuration)                      // ע�� httpClient ����
                .AddGoldCloudDataBase(Configuration)                        // ע�� ���ݿ� ����
                .AddGoldCloudOptions(Configuration)                         // ע�� ϵͳ����
                .AddPermissionHealthChecks()                                // ע�� HealthCheck
                .AddClusterClientHandler<ApiClusterClientHandler>()
                .AddConsulClustering(Configuration);


            #region ��ʼ��ϵͳ��������

            //services.InitializeSystem();

            #endregion
        }

        #region �м�����ܵ�����

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
