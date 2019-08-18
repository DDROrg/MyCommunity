using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MyCommunity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //var env = hostingContext.HostingEnvironment;
                    //config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    //config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    // Call additional providers here as needed.
                    // Call AddEnvironmentVariables last if you need to allow environment
                    // variables to override values from other providers.
                    //config.AddEnvironmentVariables(prefix: "DEB_");
                    //config.AddEnvironmentVariables();
                })
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    // removes all providers from LoggerFactory
                    logging.ClearProviders(); 
                    logging.AddConsole();
                    // Add Trace listener provider
                    logging.AddTraceSource("Information, ActivityTracing"); 
                });
    }
}
