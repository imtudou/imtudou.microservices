using Imtudou.OrderServer.WebAPI.Application.Order;
using Imtudou.OrderServer.WebAPI.Handler;

using MediatR;
namespace Imtudou.OrderServer.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            });
            builder.Services.AddMediatR(typeof(EditProductNumHandler).Assembly);
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IOrderService, OrderService>();

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

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}