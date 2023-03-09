using GoldCloud.Infrastructure.DataBase.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoldCloud.Permissions.Api.HealthChecks
{
    /// <summary>
    /// Health Check
    /// </summary>
    public class BaseHealthCheck : IHealthCheck
    {
        private IClusterClient ClusterClient;

        private IServiceProvider ServiceProvider;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="clusterClient"></param>
        /// <param name="serviceProvider"></param>
        public BaseHealthCheck(IClusterClient clusterClient, IServiceProvider serviceProvider)
        {
            ClusterClient = clusterClient;
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Health check async
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var db = ServiceProvider.GetGoldPermissionDB();
                if (db.Connection.State is not System.Data.ConnectionState.Open)
                    db.Connection.Open();

                ClusterClient.GetUniqueIdService().GetPrimaryKey();
                ClusterClient.Connect((ex) =>
                {
                    throw ex;
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(HealthCheckResult.Unhealthy(ex.Message));
            }

            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}