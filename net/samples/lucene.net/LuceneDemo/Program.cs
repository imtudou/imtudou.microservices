using Imtudou.Core.Data.SqlSugar;

using LuceneDemo.Entity;

namespace LuceneDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // 1.���mvc
            builder.Services.AddMvc();

            builder.Services.AddTransient<ISqlSugarRepository<SkuEntity, string>, SqlSugarRepository<SkuEntity, string>>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 2.���·��
            app.UseRouting();
            app.UseAuthorization();

            // 3.����ս��
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run();
        }
    }
}