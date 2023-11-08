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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // �Ѿ��ر��� JWT Claim����ӳ�䣬�������õ�Claim������'sub'��'idp'��

            services.AddAuthentication(options => // �������֤������ӵ� DI
            {
                options.DefaultScheme = "Cookies"; //ʹ�� cookie �����ص�¼�û���ͨ����Cookies����ΪDefaultScheme��
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies") // ��ӿ��Դ��� cookie �Ĵ������
            .AddOpenIdConnect("oidc", options =>   // ����ִ�� OpenID Connect Э��Ĵ������
            {
                options.Authority = "https://localhost:5000"; // ���ε� IdentityServer ��ַ(Ϊ�˱����鷳���ֱ�����ó�https)
                options.RequireHttpsMetadata = false;
                options.ClientId = "Mvc.Client";
                options.ClientSecret = "secret";
                options.SaveTokens = true; // �� cookie �б�������IdentityServer ������

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
                        // Զ��ʧ�ܵ��¼�������ͼ������û��ܾ�����ô����ת��ָ����ҳ��
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

            app.UseAuthentication();  // �����֤�м��

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
