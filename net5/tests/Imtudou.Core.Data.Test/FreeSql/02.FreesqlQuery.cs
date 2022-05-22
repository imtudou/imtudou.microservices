using Microsoft.Extensions.Configuration;

using System;   
using Microsoft.Extensions.DependencyInjection;

using Xunit;
using System.Linq;
using System.IO;
using FreeSql;

namespace Imtudou.Core.Data.Test.FreeSql
{
    public class FreesqlQuery
    {
        private readonly string mysql;
        private readonly string sqlserver;
        private IServiceProvider serviceProvider;

        public FreesqlQuery()
        {
            mysql = GetConfiguration().GetConnectionString("mysql");
            sqlserver = GetConfiguration().GetConnectionString("sqlserver");
            serviceProvider = ConfigureService();
        }

        [Fact]
        public void PageQuery()
        {
            var fsql = serviceProvider.GetService<IFreeSql>();

            // 单标查询

            var list1 = fsql.Select<BlogEntity>()
                .Where(s => s.ID == 10)
                .ToList();
            var list2 = fsql.Select<BlogEntity>()
                .Where(s => new[] { 1, 2, 3 }.Contains(s.ID))
                .ToList();

            // withsql

            var list3 = fsql.Select<BlogDetailEntity>()
                .WithSql("select * from BlogDetail where BlogDetailID = ?val", new { val = "7eb518d1-acf3-4ae3-a97b-77a9ff53df47" })
                .ToList();
        }

        [Fact]
        public void JoinQuery()
        {
            //导航属性联表
            var fsql = serviceProvider.GetService<IFreeSql>();
            var list1 = fsql.Select<BlogEntity, BlogDetailEntity>()
                 .LeftJoin((b, bd) => b.BlogID == bd.BlogID)
                 .Where((b, bd) => b.ID > 0)
                 .ToList((b, bd) => new
                 {
                     b.BlogID,
                     b.Url,
                     b.Rating,
                     bd.Title,
                     bd.Content,
                     b.CreateTime
                 });

            var list2 = fsql.Select<BlogEntity>().From<BlogDetailEntity>((b, bd) => b
                 .LeftJoin(s => s.BlogID == bd.BlogID)
                 .Where(s => s.ID == bd.ID))
                 //.ToList((b, bd) => new
                 //{
                 //    b.BlogID,
                 //    b.Url,
                 //    b.Rating,
                 //    bd.Title,
                 //    bd.Content,
                 //    b.CreateTime

                 //})
                 .ToList((b, bd) => new 
                 { 

                 
                 });


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
