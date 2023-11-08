

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NLog.Web;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imtudou.ProductServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NLogBuilder.ConfigureNLog("Configs\\NLog.config");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // �ڹ�������ʱ������á�UseServiceProviderFactory��new AutofacServiceProviderFactory��������`���򽫲�����ô�
                //.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                //.ConfigureAppConfiguration((hostingContext, config) =>
                //{
                //    config.AddJsonFile("Configs\\appsettings.json", true, true)
                //   //.AddJsonFile($"Configs\\appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
                //   ;
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
    }
}
