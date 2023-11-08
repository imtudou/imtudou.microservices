using AgileConfig.Client;

using Imtudou.Core.Helpers;
using Imtudou.IdentityServer.Models;
using Imtudou.IdentityServer.Web;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imtudou.IdentityServer.Controllers
{

    [Route("api/[controller]/[action]")]
    public class HomePageController : ControllerBase
    {
        public IConfiguration Configuration { get; private set;}
        private AppsettingsOptions _options;
        private readonly IConfiguration _IConfiguration;

        public HomePageController(IOptionsSnapshot<AppsettingsOptions> options, IConfiguration configuration)
        {
            this._options = options.Value;
            _IConfiguration = configuration;
        }

        [HttpPost]
        public IActionResult GetUser([FromBody] UserInfoModel input)
        {
            return Ok(DateTime.Now);
        }

        [HttpGet]
        public IActionResult Test1()
        {
            // 无法热更新
            var config1 = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = "Configs//appsettings.json", Optional = true, ReloadOnChange = true })
                .Build();
            var mysql = config1.GetConnectionString("mysql");
            var test = config1.GetSection("test").Value;
            var result = new List<TestOutPut>
            {
                new TestOutPut   { Key = "mysql", Value = mysql  },
                new TestOutPut   { Key = "test", Value = test  }
            };

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Test2()
        {

            // 无法热更新
            var config2 = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = "Configs//appsettings.json", Optional = true, ReloadOnChange = true })
                .Add(new AgileConfigSource(new ConfigClient("Configs//appsettings.json")))
                .Build();
            config2.Reload();
            Configuration = config2;
            var mysql = Configuration.GetConnectionString("mysql");
            var test = Configuration.GetSection("test").Value;
            var result = new List<TestOutPut>
            {
                new TestOutPut   { Key = "mysql", Value = mysql  },
                new TestOutPut   { Key = "test", Value = test  }
            };

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Test3()
        {          
            // 无法热更新
            var config3 = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = "Configs//appsettings.json", Optional = true, ReloadOnChange = true })
                .Build();
            config3.Bind(new AppsettingsOptions());

            var mysql = config3.GetConnectionString("mysql");
            var test = config3.GetSection("test").Value;
            var result = new List<TestOutPut>
            {
                new TestOutPut   { Key = "mysql", Value = mysql  },
                new TestOutPut   { Key = "test", Value = test  }
            };

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Test4()
        {
            // 可以更新

            // 1. 添加一个AppsettingsOptions.cs 与配置文件数据结构一样
            // 2. Program.cs 配置读取json文件路径
            //.ConfigureAppConfiguration((hostingContext, config) =>
            //{
            //    config.AddJsonFile("Configs\\appsettings.json", true, true)
            //   .AddJsonFile($"Configs\\appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
            //   ;
            //})
            // 3. Startup.cs 配置到容器中 services.Configure<AppsettingsOptions>(Configuration.GetSection("AppsettingsOptions"))
            // 5. 通过 IOptionsSnapshot<AppsettingsOptions>.value 读取

            var test = _options.test;
            var result = new List<TestOutPut>
            {
                new TestOutPut   { Key = "test", Value = test  }
            };

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Test5()
        {
            var test = AppSettingsHelper.Configuration.GetSection("test").Value;
            var xtesst = AppSettingsHelper.Configuration.GetSection("xtesst").Value;
            var result = new List<TestOutPut>
            {
                new TestOutPut   { Key = "test", Value = test  },
                new TestOutPut   { Key = "xtesst", Value = xtesst  }
            };

            return Ok(result);

        }

        [HttpGet]
        public IActionResult Test6()
        {
            var test = _IConfiguration.GetSection("secret").Value;
            var result = new List<TestOutPut>
            {
                new TestOutPut   { Key = "test", Value = test  }
            };

            return Ok(result);

        }

        



    }

    public class TestOutPut
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public string secret { get; set; }
    }
}
