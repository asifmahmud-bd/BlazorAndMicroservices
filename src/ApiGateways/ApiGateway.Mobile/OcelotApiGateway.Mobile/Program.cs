using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OcelotApiGateway.Mobile
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContex, config) =>
                {
                    config.AddJsonFile($"ocelot.{hostingContex.HostingEnvironment.EnvironmentName}.json", true, true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((hostingContext, loggingbuilder) =>
                {
                    loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    loggingbuilder.AddConsole();
                    loggingbuilder.AddDebug();
                });
    }
}
