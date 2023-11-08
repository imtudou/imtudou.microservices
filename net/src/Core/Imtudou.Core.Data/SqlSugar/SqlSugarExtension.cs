using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SqlSugar;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imtudou.Core.Data.SqlSugar
{

    /// <summary>
    /// SqlSugar.IOC/依赖注入
    /// </summary>
    public static class SqlSugarExtension
    {
        /// <summary>
        /// AddSqlSugar
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="configAction"></param>
        public static void AddSqlSugar(this IServiceCollection services, IConfiguration configuration, Action<List<ConnectionConfig>> configAction)
        {
            if (configAction == null)
            {
                throw new SqlSugarException("配置错误");
            }
            var cconfig = new List<ConnectionConfig>();
            configAction = s =>
            {
                cconfig = s;
            };
            SqlSugarScope sqlSugar = new SqlSugarScope(cconfig);
            // 验证连接是否成功
            if (!sqlSugar.Ado.IsValidConnection())
            {
                throw new SqlSugarException("SqlSugarClient 连接失败，请检查连接字符串或者数据库类型是否正确");
            }
            services.AddSingleton<ISqlSugarClient>(sqlSugar);//这边是SqlSugarScope用AddSingleton
        }

        /// <summary>
        /// UseSqlSugar
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="configAction"></param>
        public static void UseSqlSugar(this IApplicationBuilder app)
        {

        }

    }
}
