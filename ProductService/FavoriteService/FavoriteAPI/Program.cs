using BLL.Products;
using BLL.Products.Abstractions;
using DAL;
using DAL.Abstractions;
using DAL.ConfigSettings;
using Microsoft.OpenApi.Models;

namespace FavoriteAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Конфигурация
            configuration.AddJsonFile("Properties/secretsSettings.json");

            // Регистрация сервисов
            builder.Services.Configure<RedisOptions>(configuration.GetSection(nameof(RedisOptions)));
            builder.Services.AddSingleton<IContextManager, ContextManager>();
            builder.Services.AddSingleton<IAppSettings, AppSettings>();
            builder.Services.AddSingleton<ISecretsSettings, SecretsSettings>();
            builder.Services.AddTransient<IFavoriteProductService, FavoriteProductService>();

            // Добавление CORS (ДО builder.Build()!)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://localhost:7100") // URL вашего фронтенда
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // Если используете куки/JWT
                });
            });

            // Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1",
                    Description = "Пример API с Swagger",
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting(); // Важно: UseRouting ДО UseCors

            // Активация CORS (после UseRouting, до UseAuthorization)
            app.UseCors("AllowFrontend");

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}