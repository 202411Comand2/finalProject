using ClusterService.BLL;
using ClusterService.DAL;
using Microsoft.OpenApi.Models;
using Platform.DAL;

namespace ClusterAPI
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
            services.AddTransient<IClusterMainService, ClusterService.BLL.ClusterMainService>();
            services.AddTransient<ISearchClusterService, SearchClusterService>();

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

                //Для разговора с Глебом
                //// Добавляем JWT-аутентификацию (опционально)
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorization header. Example: \"Bearer {token}\"",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});
            });

            var app = builder.Build();

            // Настройка middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}
