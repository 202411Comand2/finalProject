using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotWebApiGr
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Конфигурация Ocelot
            builder.Configuration.AddJsonFile("Properties/ocelot.json");
            builder.Services.AddOcelot();

            // Настройка CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerForOcelot(builder.Configuration);

         
            var app = builder.Build();
            app.UseCors("AllowAll");
            await app.UseOcelot();
            app.UseSwagger();
            app.UseSwaggerUi(opt => {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });
            app.Run();
        }
       
    }
}
