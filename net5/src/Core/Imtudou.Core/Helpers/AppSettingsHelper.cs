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
        /// <summary>
        /// GetConfiguration
        /// </summary>
        /// <returns></returns>
        public static IConfiguration GetConfiguration(string jsonName = "appsettings.json")
        {
            // Microsoft.Extensions.Configuration.Json
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Configs\\{jsonName}")))
            {

                configurationBuilder
                    .AddJsonFile($"Configs\\{jsonName}", true, true)
                    .AddJsonFile($"Configs\\{jsonName}.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
            }

            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{jsonName}")))
            {
                configurationBuilder
                   .AddJsonFile($"{jsonName}", true, true)
                   .AddJsonFile($"{jsonName}.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
            }
            return configurationBuilder.Build(); ;
        }
    }
}
