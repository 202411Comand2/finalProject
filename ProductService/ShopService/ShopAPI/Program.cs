using BLL.Product;
using BLL.Products.Abstractions;
using DAL;
using DAL.Abstractions;
using DAL.ConfigSettings;
using Microsoft.OpenApi.Models;

namespace ShopAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Конфигурация
            builder.Configuration.AddJsonFile("Properties/secretsSettings.json");

            // Регистрация сервисов
            builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection(nameof(RedisOptions)));
            builder.Services.AddSingleton<IContextManager, ContextManager>();
            builder.Services.AddSingleton<IAppSettings, AppSettings>();
            builder.Services.AddSingleton<ISecretsSettings, SecretsSettings>();
            builder.Services.AddTransient<IShopService, ShopService>();

            // Настройка CORS (разрешаем фронтенд на 7100 порту)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://localhost:7100") // URL вашего фронтенда
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // Для работы с куки/JWT
                });
            });

            // Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Shop API",
                    Version = "v1",
                    Description = "API для работы с магазином",
                    Contact = new OpenApiContact { Name = "Dev", Email = "dev@example.com" }
                });
            });

            var app = builder.Build();

            // Middleware pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop API V1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting(); // Важно: должно быть перед UseCors

            // Активация CORS (после UseRouting, до UseAuthorization)
            app.UseCors("AllowFrontend");

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}