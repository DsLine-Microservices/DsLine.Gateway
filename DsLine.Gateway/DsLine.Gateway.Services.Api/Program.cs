using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;

namespace DsLine.Gateway.Services.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot Configuration = null;

            CreateHostBuilder(args, Configuration).Build().Run();
        }



        public static IHostBuilder CreateHostBuilder(string[] args, IConfigurationRoot Configuration) =>

            Host.CreateDefaultBuilder(args)

                .ConfigureWebHostDefaults(webBuilder =>

                {
               
                    webBuilder.UseStartup<Startup>();

                }).ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true,
                            true)
                        .AddJsonFile("ocelot.json", false, false)
                        .AddEnvironmentVariables();

                    Configuration = config.Build();

                }).ConfigureServices(s =>
                {
                    s.AddOcelot().AddConsul();
                });

        
    }
}
