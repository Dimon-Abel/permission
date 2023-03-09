using GoldCloud.Infrastructure.Swagger;
using GoldCloud.Permissions.Api.HealthChecks;
using GoldCloud.Permissions.Api.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GoldCloud.Permissions.Api.Extensions
{
    /// <summary>
    /// 服务拓展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册 Orleans 集群
        /// </summary>
        /// <param name="services">      </param>
        /// <param name="configuration"> </param>
        /// <returns> </returns>
        public static IServiceCollection AddOrleans(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAdoNetClustering(configuration.GetSection("AdoNetClusteringClientOptions"), configuration.GetSection("ClusterOptions"));

            return services;
        }

        /// <summary>
        /// 添加系统配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"> </param>
        /// <returns></returns>
        public static IServiceCollection AddGoldCloudOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RemoteApiConfigOptions>(configuration.GetSection(nameof(RemoteApiConfigOptions)));

            return services;
        }

        /// <summary>
        /// 添加缓存服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDistributedCacheService(this IServiceCollection services)
        {
            // 使用基于内存的缓存
            //TODO 后期可以根据需要 更换为 Redis 缓存
            // https://docs.microsoft.com/zh-cn/aspnet/core/performance/caching/distributed?view=aspnetcore-5.0#idistributedcache-interface
            // https://www.nuget.org/packages/Microsoft.Extensions.Caching.StackExchangeRedis
            services.AddDistributedMemoryCache();

            return services;
        }

        /// <summary>
        /// 添加HttpClient 服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddGoldCloudHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            return services;
        }

        /// <summary>
        /// 添加 HealthCheck
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddPermissionHealthChecks(this IServiceCollection services)
        {
            var healthChecksBuilder = services.AddHealthChecks()
                .AddCheck<BaseHealthCheck>("base_check");
            return services;
        }
    }
}
