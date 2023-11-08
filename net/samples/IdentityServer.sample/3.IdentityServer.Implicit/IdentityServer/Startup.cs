

using IdentityServer;

using IdentityServer4;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3.IdentityServer.Implicit.IdentityServer
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

            #region ÅäÖÃ IdentityServer
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryApiScopes(Config.GetApiScopes()) // ÐÂ°æÐèÒªÅäÖÃ
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers())
                ;
            #endregion

            services.AddAuthentication()
               .AddOpenIdConnect("oidc", "OpenID Connect", options =>
               {
                   options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                   options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                   options.SaveTokens = true;

                   options.Authority = "https://demo.identityserver.io/";
                   options.ClientId = "implicit";

                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       NameClaimType = "name",
                       RoleClaimType = "role"
                   };
               });
            services.AddControllersWithViews();

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

            app.UseIdentityServer();
            app.UseStaticFiles();
         
            app.UseRouting();

            app.UseAuthentication();
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
