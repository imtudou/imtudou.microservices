using FreeSql;
using FreeSql.DataAnnotations;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;

using Xunit;

namespace Imtudou.Core.Data.Test
{
    public class UnitTest1
    {
        private readonly string mysql;
        private readonly string sqlserver;
        private IServiceProvider serviceProvider;

        public UnitTest1()
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
            for (int i = 0; i < 100; i++)
            {
                var input = new Blog();
                input.BlogId = (int)fsql.Insert<Blog>().AppendData(input).ExecuteIdentity();
                Assert.True(input.BlogId > 0);
            }
            
        }

        [Fact]
        public void AddOrEdit()
        {
            var fsql = serviceProvider.GetService<IFreeSql>();
            var input = new Blog {Url = "http://www.freesql.net/",Rating = 2073783301 };
            var result = fsql.InsertOrUpdate<Blog>().SetSource(input).ExecuteAffrows();
            Assert.True(result > 0);
        }




        [Fact]
        public void Edit()
        {
            var fsql = serviceProvider.GetService<IFreeSql>();
            var rows = fsql.Update<Blog>()
                .Set(s => s.Url, "http://www.imtudou.cn")
                .Where(s => s.BlogId == 11)
                .ExecuteAffrows();

            Assert.True(rows == 1);
        }

        [Fact]
        public void Remove()
        {
            var fsql = serviceProvider.GetService<IFreeSql>();
            var rows = fsql.Delete<Blog>()
                .Where(s => s.BlogId == 11)
                .ExecuteAffrows();
            Assert.True(rows == 1);
        }


        [Fact]
        public void Query()
        {
            var pageIndex = 1;
            var pageSize = 10;
            var fsql = serviceProvider.GetService<IFreeSql>();
            var result = fsql.Select<Blog>()
                .Where(s => s.Rating > 0)
                .OrderBy(s => s.BlogId)
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
            var result = fsql.Select<Blog>()
                .Where(s => s.Rating > 0)
                .OrderBy(s => s.BlogId)
                .Skip( pageIndex * pageSize)
                .Limit(pageSize) //第50行-60行的记录
                .ToList();

            Assert.NotNull(result);
        }

        private static IConfiguration GetConfiguration()
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

    public class Blog
    {
        public Blog()
        {
            Url = "www.baidu.com";
            Rating = new Random().Next();
        }


        [Column(IsIdentity = true, IsPrimary = true)]
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
    }

    public class BlogDetail
    {
        public BlogDetail(int blogid)
        {
            Title = "标题" + new Random().Next(1, 99);
            BlogId = blogid;
            Content = "内容" + new Random().Next(1, 99);
        }

        [Column(IsIdentity = true, IsPrimary = true)]
        public int BlogDetailID { get; set; }
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
