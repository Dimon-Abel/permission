using Consul;
using GoldCloud.Domain.Handlers;
using GoldCloud.Domain.Impls.Grain;
using GoldCloud.Infrastructure.DataBase.Extensions;
using KingMetal.Domains.UniqueValueService.Grains;
using KingMetal.Infrastructures.Common.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GoldCloud.Permissions
{
    public class Program
    {
        #region 主程序入口

        /// <summary>
        /// 主程序入口
        /// </summary>
        /// <returns> </returns>
        public static Task Main(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                  .UseContentRoot(Directory.GetCurrentDirectory())
                  .ConfigureAppConfiguration((builder) =>
                  {
#if DEBUG
#else
                builder.AddRemoteConfiguration();
#endif
                      builder.AddUserSecrets<Program>();
                  })
                  .ConfigureHostConfiguration(configBuilder => configBuilder.AddEnvironmentVariables(prefix: "ASPNETCORE_"))
                  .UseOrleans((context, siloBuilder) =>
                  {
                      siloBuilder
                          .UseDashboard(options => options.Port = context.Configuration.GetSection("DashboardOptions").GetValue("Port", 8350))
                          .UseAdoNetReminderService(context.Configuration)
                          .UseConsulClustering(context.Configuration)
                          .AddKingMetal(context.Configuration)
                          .AddKingMetalConsumer()
                          .AddUniqueIdService()
                          .AddUniqueValueService()
                          .AddTransactionService(context.Configuration)
                          .AddPostgreSQLUniqueIdStorage(context.Configuration)
                          .AddPostgreSQLEventStorage(context.Configuration)
                          .AddPostgreSQLSnapshotStorage(context.Configuration)
                          .AddPostgreSQLCommandStorage(context.Configuration)
                          .AddPostgreSQLObserverStateStorage(context.Configuration)
                          .AddPostgreSQLUniqueValueStorage(context.Configuration)
                          .AddPostgreSQLTransactionStorage(context.Configuration)
                          .AddRabbitMQMessagBus(context.Configuration)
                          .Configure<GrainCollectionOptions>(options => options.CollectionAge = TimeSpan.FromMinutes(30))
                          .ConfigureServices(m => m.AddGoldCloudDataBase(context.Configuration))
                          .ConfigureServices(m => m.AddSingleton(context.Configuration))
                          .AddSimpleMessageStreamProvider("PermissionStreamProvider", options =>
                          {
                              options.FireAndForgetDelivery = true;
                              options.PubSubType = Orleans.Streams.StreamPubSubType.ExplicitGrainBasedOnly;// 消息
                          })
                          .AddMemoryGrainStorage("PubSubStore")
                          .ConfigureServices(services =>
                          {
                          })
                          .ConfigureApplicationParts(parts =>
                          {
                              parts.AddApplicationPart(typeof(UniqueValueServiceGrain).Assembly).WithReferences();
                              parts.AddApplicationPart(typeof(MenuGrain).Assembly).WithReferences();
                              parts.AddApplicationPart(typeof(MenuDbGrain).Assembly).WithReferences();
                          });
                  })
                  .ConfigureLogging(logging =>
                  {
                      logging.ClearProviders();
                      logging.AddNLog("nlogsettings.config");
                  })
                  .Build()
                  .RunAsync();
        }

        #endregion 主程序入口
    }
}
