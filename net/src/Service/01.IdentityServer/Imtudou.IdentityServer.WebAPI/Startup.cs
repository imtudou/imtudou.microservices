using FluentValidation;

using Imtudou.Core.DependencyInjection;
using Imtudou.Core.Extensions.Filters;
using Imtudou.IdentityServer.Application.Authorization.Accounts;
using Imtudou.IdentityServer.Application.Authorization.Accounts.Dto;
using Imtudou.IdentityServer.Controllers;
using Imtudou.IdentityServer.Web;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using System;
using System.Reflection;

namespace Imtudou.IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region 配置IdentityServer4
            services.AddIdentityServer()
                .AddDeveloperSigningCredential() // 配置证书
                .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes()) // 配置API允许访问的范围
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources()) // 配置身份资源
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources()) // 配置受保护的资源列表
                .AddInMemoryClients(IdentityServerConfig.GetClients())  // 配置允许验证的Client
                ;
            #endregion

            //services.Configure<AppsettingsOptions>(Configuration.GetSection("AppsettingsOptions"));

            services.AddControllers();
            services.AddMvc(s =>
            {
                s.Filters.Add(typeof(APIActionFilter));
                s.Filters.Add(typeof(APIExceptionFilter));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Imtudou.IdentityServer", Version = "v1" });
            });
            services.AddHealthChecks();
            services.Configure<AppsettingsOptions>(this.Configuration);
            services.AddValidatorsFromAssemblyContaining<RegisterInputValidator>();
            services.AddServiceFromAssemblyContaining<IAccountService>();
            //services.AddScoped<IAccountService, AccountService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Imtudou.IdentityServer"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                {
                    ResponseWriter = (context, health) =>
                    {
                        return context.Response.WriteAsync($"Imtudou.IdentityServer is {health.Status}!{DateTime.Now}");
                    }                
                });// https://www.cnblogs.com/uoyo/p/12396644.html
            });
        }
    }
}
