using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace GoldCloud.Permissions.Api
{
    /// <inheritdoc />
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"> </param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Host
        /// </summary>
        /// <param name="args"> </param>
        /// <returns> </returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    builder.AddEnvironmentVariables(prefix: "ASPNETCORE_");
#if DEBUG 
#else
                builder.AddRemoteConfiguration();
#endif
                    builder.AddUserSecrets<Program>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
    }
}
