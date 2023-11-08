using AgileConfig.Client;

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using NLog.Web;

using System;

namespace Imtudou.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //NLogBuilder.ConfigureNLog("Configs\\NLog.config");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // 在构建主机时必须调用“UseServiceProviderFactory（new AutofacServiceProviderFactory（））”`否则将不会调用此
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     config.AddJsonFile("Configs\\appsettings.json", true, true)
                    .AddJsonFile($"Configs\\appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
                    ;
                 })
                //.UseAgileConfig(new ConfigClient($"Configs\\appsettings.json"), e => Console.WriteLine($"configs {e.Action}"))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
    }
}
