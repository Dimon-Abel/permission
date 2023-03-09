//using KingMetal.Domains.Extensions.Hanlders;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Orleans;
//using System;
//using System.Threading.Tasks;

//namespace GoldCloud.Permissions.Api.Handlers
//{
//    /// <summary>
//    ///     Api ClusterClient Handler
//    /// </summary>
//    public class ApiClusterClientHandler : ClusterClientHandler
//    {
//        #region 初始化

//        /// <summary>
//        ///     集群客户端连接相关的处理器
//        /// </summary>
//        /// <param name="serviceProvider"></param>
//        public ApiClusterClientHandler(IServiceProvider serviceProvider)
//        {
//            ServiceProvider = serviceProvider;
//        }

//        #endregion

//        #region 连接成功后事件

//        /// <summary>
//        ///     连接成功后事件
//        /// </summary>
//        /// <param name="clusterClient"></param>
//        /// <returns></returns>
//        public override Task OnConnectCompleted(IClusterClient clusterClient)
//        {
//            Logger.LogInformation("连接到集群成功");
//            return Task.CompletedTask;
//        }

//        #endregion

//        #region 连接失败的事件，如果返回true则自动重连

//        /// <summary>
//        ///     连接失败的事件，如果返回true则自动重连
//        /// </summary>
//        /// <param name="ex"></param>
//        /// <returns></returns>
//        public override async Task<bool> OnConnectFailed(Exception ex)
//        {
//            const int delay = 1;
//            Logger.LogWarning($@"连接到集群失败 {ex.Message}，{delay}s后将重试");
//            await Task.Delay(delay * 1000);
//            return true;
//        }

//        #endregion

//        #region 连接断开后的事件

//        /// <summary>
//        ///     连接断开后的事件
//        /// </summary>
//        /// <param name="clusterClient"></param>
//        /// <param name="sender"></param>
//        /// <param name="args"></param>
//        /// <returns></returns>
//        public override async Task<bool> OnDisconnected(IClusterClient clusterClient, object sender, EventArgs args)
//        {
//            Logger.LogWarning("集群连接已断开");
//            await Task.Delay(0);
//            return true;
//        }

//        #endregion

//        #region 集群网关的数量发生变化的事件

//        /// <summary>
//        ///     集群网关的数量发生变化的事件
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="args"></param>
//        /// <returns></returns>
//        public override Task OnGatewayCountChanged(object sender, GatewayCountChangedEventArgs args)
//        {
//            Logger.LogInformation(
//                $"集群网关数量发生变化 {args.PreviousNumberOfConnectedGateways}变为{args.NumberOfConnectedGateways}");
//            return base.OnGatewayCountChanged(sender, args);
//        }

//        #endregion

//        #region 集群连接重连成功的事件

//        /// <summary>
//        ///     集群连接重连成功的事件
//        /// </summary>
//        /// <param name="clusterClient"></param>
//        /// <returns></returns>
//        public override async Task OnReconnectCompleted(IClusterClient clusterClient)
//        {
//            await Task.Delay(0);
//            Logger.LogInformation("集群重新连接成功----");
//        }

//        #endregion

//        #region 集群连接重连失败的事件，返回true则继续重连

//        /// <summary>
//        ///     集群连接重连失败的事件，返回true则继续重连
//        /// </summary>
//        /// <param name="ex"></param>
//        /// <param name="clusterClient"></param>
//        /// <returns></returns>
//        public override async Task<bool> OnReconnectFailed(Exception ex, IClusterClient clusterClient)
//        {
//            const int delay = 1;
//            Logger.LogWarning($"集群连接重连失败 {ex.Message}，{delay}s后将重试");
//            await Task.Delay(delay * 1000);
//            return true;
//        }

//        #endregion 

//        #region 成员变量

//        /// <summary>
//        ///     日志对象
//        /// </summary>
//        private ILogger<ApiClusterClientHandler> Logger =>
//            ServiceProvider.GetRequiredService<ILogger<ApiClusterClientHandler>>();


//        /// <summary>
//        ///     服务提供程序
//        /// </summary>
//        protected IServiceProvider ServiceProvider;

//        #endregion
//    }
//}
