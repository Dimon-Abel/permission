using System;
using System.Runtime.CompilerServices;
using GoldCloud.Infrastructure.DataBase.Options;
using KingMetal.Infrastructures.Common.Utilities;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;


namespace GoldCloud.Infrastructure.DataBase.Extensions
{
    public static class ServiceCollectionExtensions
    {
        #region 添加数据库

        /// <summary>
        /// 添加数据库
        /// </summary>
        /// <param name="services"> </param>
        /// <param name="config">   配置委托 </param>
        /// <returns> </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddGoldCloudDataBase(this IServiceCollection services, Action<PermissionDataBaseOptions> config)
        {
            var terminalDataBaseOptions = new PermissionDataBaseOptions();
            config(terminalDataBaseOptions);

            if (string.IsNullOrEmpty(terminalDataBaseOptions.ConnectionString))
                throw new ArgumentException("ConnectionString Can Not Be Null");

            services.AddLinqToDBContext<GoldPermissionDB>((provider, options) =>
            {
                options.UsePostgreSQL(terminalDataBaseOptions.ConnectionString);

                var environment = provider.GetService<IHostEnvironment>();
                if (!environment.IsProduction())
                {
                    options.UseDefaultLogging(provider);
                }
            }, ServiceLifetime.Singleton);

            services.Replace(new ServiceDescriptor(typeof(GoldPermissionDB),
                provider =>
                {
                    var options = provider.GetService<LinqToDBConnectionOptions<GoldPermissionDB>>();
                    return new GoldPermissionDB(options);
                }, ServiceLifetime.Transient));

            //禁用LinqToDB缓存
            LinqToDB.Common.Configuration.Linq.DisableQueryCache = true;

            return services;
        }

        /// <summary>
        /// 添加数据库
        /// </summary>
        /// <param name="services">      </param>
        /// <param name="configuration"> 配置对象 </param>
        /// <returns> </returns>
        public static IServiceCollection AddGoldCloudDataBase(this IServiceCollection services, IConfiguration configuration)
            => services.AddGoldCloudDataBase(m =>
            {
                var cfg = ConfigurationHelper.GetConfiguration(configuration, "PermissionDataBaseOptions");
                m.ConnectionString = cfg.GetValue<string>("ConnectionString");
            });

        #endregion 添加数据库

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="service"> </param>
        /// <returns> </returns>
        public static GoldPermissionDB GetGoldPermissionDB(this IServiceProvider service) => service.GetService<GoldPermissionDB>();
    }
}
