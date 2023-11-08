using AgileConfig.Client;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

using NPOI.OpenXml4Net.OPC;

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
        public static IConfiguration Configuration { get; }

        static AppSettingsHelper()
        {
            // Microsoft.Extensions.Configuration.Json
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Configs\\appsettings.json")))
            {
                configurationBuilder
                    .AddJsonFile($"Configs\\appsettings.json", true, true)
                    .AddJsonFile($"Configs\\appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true); 
            }

            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"appsettings.json")))
            {
                configurationBuilder
                   .AddJsonFile($"appsettings.json", true, true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);
             
            }

            //IEnumerable<IConfigurationSection> children = configurationBuilder.Sources.Contains("AgileConfig").GetChildren();
            //if (children == null || !children.Any())
            //{
            //    configurationBuilder.Add(new AgileConfigSource(new ConfigClient("appsettings.json")));
            //}


            Configuration = configurationBuilder.Build();
        }

    }
}
