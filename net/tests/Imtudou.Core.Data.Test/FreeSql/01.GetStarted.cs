using FreeSql;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;
using System.Threading;

using Xunit;

namespace Imtudou.Core.Data.Test
{
    public class GetStarted
    {
        private readonly string mysql;
        private readonly string sqlserver;
        private IServiceProvider serviceProvider;

        public GetStarted()
        {
            mysql = GetConfiguration().GetConnectionString("mysql");
            sqlserver = GetConfiguration().GetConnectionString("sqlserver");
            serviceProvider = ConfigureService();
        }

        [Fact]
        public void Add()
        {
            var cc = DateTime.UtcNow.Ticks.ToString();

            var fsql = serviceProvider.GetService<IFreeSql>();
            for (int i = 0; i < 2000; i++)
            {
                Thread.Sleep(1);
                var input = new BlogEntity
                {
                    BlogID = Guid.NewGuid(),
                    Url = $"{i}.baidu.com",
                    Rating = new Random().Next(1, 3),
                    CreateTime = DateTime.Now
                };

                var id = (int)fsql.Insert<BlogEntity>()
                    .AppendData(input)
                    .ExecuteIdentity();
                fsql.Insert<BlogDetailEntity>()
                    .AppendData(new BlogDetailEntity(input.BlogID))
                    .ExecuteAffrows();
                Assert.True(id > 0);
            }
            
        }


        [Fact]
        public void AddOrEdit()
        {
            var fsql = serviceProvider.GetService<IFreeSql>();
            var input = new BlogEntity {Url = "http://www.freesql.net/",Rating = 2073783301 };
            var result = fsql.InsertOrUpdate<BlogEntity>().SetSource(input).ExecuteAffrows();
            Assert.True(result > 0);
        }




        [Fact]
        public void Edit()
        {
            var fsql = serviceProvider.GetService<IFreeSql>();
            var rows = fsql.Update<BlogEntity>()
                .Set(s => s.Url, "http://www.imtudou.cn")
                .Where(s => s.ID == 11)
                .ExecuteAffrows();

            Assert.True(rows == 1);
        }

        [Fact]
        public void Remove()
        {
            var fsql = serviceProvider.GetService<IFreeSql>();
            var rows = fsql.Delete<BlogEntity>()
                .Where(s => s.ID == 11)
                .ExecuteAffrows();
            Assert.True(rows == 1);
        }


        [Fact]
        public void Query()
        {
            var pageIndex = 1;
            var pageSize = 10;
            var fsql = serviceProvider.GetService<IFreeSql>();
            var result = fsql.Select<BlogEntity>()
                .Where(s => s.Rating > 0)
                .OrderBy(s => s.ID)
                .Skip((pageIndex -1) * pageSize)
                .Take(pageSize)
                .ToList();

            Assert.NotNull(result);
        }

        [Fact]
        public void QueryLimit()
        {
            var pageIndex = 5;
            var pageSize = 10;
            var fsql = serviceProvider.GetService<IFreeSql>();
            var result = fsql.Select<BlogEntity>()
                .Where(s => s.Rating > 0)
                .OrderBy(s => s.ID)
                .Skip( pageIndex * pageSize)
                .Limit(pageSize) //第50行-60行的记录
                .ToList();

            Assert.NotNull(result);
        }

        private IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        private IServiceProvider ConfigureService()
        {
            var service = new ServiceCollection();
            var fsql = new FreeSqlBuilder()
                .UseConnectionString(DataType.MySql, mysql)
                .UseAutoSyncStructure(true)
                .Build();
            service.AddSingleton<IFreeSql>(fsql);
            return service.BuildServiceProvider();
        }
    }
}
