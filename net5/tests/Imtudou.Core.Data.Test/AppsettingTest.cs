using Imtudou.Core.Helpers;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace Imtudou.Core.Data.Test
{
    public class AppsettingTest
    {
        private readonly ITestOutputHelper _testOutput;
        public AppsettingTest(ITestOutputHelper testOutput)
        {
            this._testOutput = testOutput; 
        }

        [Fact]
        public void Test()
        {
            var test1 = AppSettingsHelper.Configuration.GetSection("test").Value;
            _testOutput.WriteLine($"test1:{test1}");
            var test2 = AppSettingsHelper.Configuration.GetSection("test").Value;
            _testOutput.WriteLine($"test2:{test2}");

        }

        [Fact]
        public void Test2()        
        {
            var config = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = true, ReloadOnChange = true })
                .Build();
            var mysql = config.GetConnectionString("mysql");
            var test = config.GetSection("test").Value;
        }



    }
}
