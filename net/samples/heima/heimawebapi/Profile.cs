using heimawebapi.Entity;
using heimawebapi.Service;

using Imtudou.Core.Data.SqlSugar;

using System.Runtime.CompilerServices;

namespace heimawebapi
{
    public static class Profile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddProfile(this IServiceCollection services)
        {
            // entity
            services.AddTransient<ISqlSugarRepository<tbSpecillVoucherEntity, string>, SqlSugarRepository<tbSpecillVoucherEntity, string>>();
            services.AddTransient<ISqlSugarRepository<tbVoucherOrderEntity, int>, SqlSugarRepository<tbVoucherOrderEntity, int>>();

            // service                                                             
            services.AddTransient<ISpecillVoucherService, SpecillVoucherService>();
            services.AddTransient<IVoucherOrderService, VoucherOrderService>();

            return services;
        }
    }
}
