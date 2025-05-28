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

            // Добавляем конфигурацию Ocelot
            builder.Configuration.AddJsonFile("Properties/ocelot.json");

            // Настройка сервисов
            builder.Services.AddOcelot();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Конфигурация middleware
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            await app.UseOcelot();
            app.Run();
        }
    }
}
