using AutoMapper;

using Imtudou.ProductServer.WebAPI;
using Imtudou.ProductServer.WebAPI.Application.Prodcuts;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Imtudou.ProductServer
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
            services.AddControllers(); 
            //services.AddMvc(s =>
            //{
            //    s.Filters.Add(typeof(APIActionFilter));
            //    s.Filters.Add(typeof(APIExceptionFilter));
            //});
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Imtudou.ProductServer", Version = "v1" });
            });
            services.AddHealthChecks();
            services.AddAutoMapper(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            });
            //services.AddServiceFromAssemblyContaining<IAccountService>();
            services.AddSingleton<IProdcutService, ProductService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();              
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Imtudou.ProductServer v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                {
                    ResponseWriter = (context, health) =>
                    {
                        return context.Response.WriteAsync($"Imtudou.ProductServer is {health.Status}!{DateTime.Now}");
                    }
                });// https://www.cnblogs.com/uoyo/p/12396644.html
            });
        }
    }
}
