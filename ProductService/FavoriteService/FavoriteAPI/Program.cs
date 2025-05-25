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
            var services = builder.Services;

            configuration.AddJsonFile("Properties/secretsSettings.json");

            services.Configure<RedisOptions>(configuration.GetSection(nameof(RedisOptions)));
            services.AddSingleton<IContextManager, ContextManager>();
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddSingleton<ISecretsSettings, SecretsSettings>();
            services.AddTransient<IFavoriteProductService, FavoriteProductService>();


            // Добавляем сервисы
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

                //для разговора с Глебом
                //////// Добавляем JWT-аутентификацию (опционально)
                //////c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //////{
                //////    Description = "JWT Authorization header. Example: \"Bearer {token}\"",
                //////    Name = "Authorization",
                //////    In = ParameterLocation.Header,
                //////    Type = SecuritySchemeType.ApiKey,
                //////    Scheme = "Bearer"
                //////});
            });

            var app = builder.Build();

            // Настройка middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = "swagger"; // Доступ по /swagger
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
