using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Helpers
{
    /// <summary>
    /// AppSettings json文件读取类
    /// </summary>
    public class AppSettingsHelper
    {
        public static IConfiguration Configuration = GetConfiguration();

        /// <summary>
        /// GetConfiguration
        /// </summary>
        /// <returns></returns>
        private static IConfiguration GetConfiguration()
        {
            // Microsoft.Extensions.Configuration.Json
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs\\appsettings.json")))
            {

                configurationBuilder
                    .AddJsonFile("Configs\\appsettings.json", true, true)
                    .AddJsonFile($"Configs\\appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
            }

            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json")))
            {
                configurationBuilder
                   .AddJsonFile("appsettings.json", true, true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
            }

            Configuration = configurationBuilder.Build();
            return Configuration;
        }
    }
}
