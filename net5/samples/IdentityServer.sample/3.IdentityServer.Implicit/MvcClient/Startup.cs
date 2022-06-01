using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Implicit.MvcClient
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
            services.AddControllersWithViews();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // 已经关闭了 JWT Claim类型映射，以允许常用的Claim（例如'sub'和'idp'）

            services.AddAuthentication(options => // 将身份认证服务添加到 DI
            {
                options.DefaultScheme = "Cookies"; //使用 cookie 来本地登录用户（通过“Cookies”作为DefaultScheme）
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies") // 添加可以处理 cookie 的处理程序
            .AddOpenIdConnect("oidc", options =>   // 配置执行 OpenID Connect 协议的处理程序
            {
                options.Authority = "https://localhost:5000"; // 信任的 IdentityServer 地址(为了避免麻烦最好直接配置成https)
                options.RequireHttpsMetadata = false;
                options.ClientId = "Mvc.Client";
                options.ClientSecret = "secret";
                options.SaveTokens = true; // 在 cookie 中保留来自IdentityServer 的令牌

                options.Events = new Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectEvents
                {
                    OnRemoteSignOut = context =>
                    {
                        context.Response.Redirect("Home");
                        context.HandleResponse();
                        return Task.FromResult(0);
                    },
                    OnRemoteFailure = context =>
                    {
                        // 远程失败的事件，如下图，如果用户拒绝，那么会跳转到指定的页面
                        context.Response.Redirect("Home");
                        context.HandleResponse();
                        return Task.FromResult(0);
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();  // 添加认证中间件

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
