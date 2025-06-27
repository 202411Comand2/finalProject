using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Platform.DAL;
using ProductService.BLL;
using ProductService.DAL;
using Rabbit.Platform;
using System.Text;

namespace ProductAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Конфигурация
            builder.Configuration
                .AddJsonFile("Properties/secretsSettings.json")
                .AddJsonFile("Properties/rabbitSettings.json");

            builder.Services.Configure<RedisOptions>(configuration.GetSection(nameof(RedisOptions)));
            // Сервисы
            builder.Services
                .AddSingleton<IContextManager, ContextManager>()
                .AddSingleton<IAppSettings, AppSettings>()
                .AddSingleton<ISecretsSettings, SecretsSettings>()
                .AddTransient<IProductMainService, ProductMainService>()
                .AddSingleton<IRabbitSettings, RabbitSettings>()
                .AddSingleton<IRabbitMQService, RabbitMQService>()
                .AddSingleton<IMessageConsumer, MessageConsumer>()
                .AddHostedService<ShopToProductMessagesConsumer>();

            // Аутентификация
            // Настройка JWT аутентификации
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                };
            });

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins("https://localhost:7100") // Мой сайт
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API", Version = "v1" });

                // Добавьте JWT в Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Введите JWT с 'Bearer '",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });

            var app = builder.Build();

            // Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll"); // ← Должно быть до UseRouting()
            app.UseRouting();
            app.UseAuthentication(); // ← Добавлено
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}