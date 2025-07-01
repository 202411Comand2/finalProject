using CommentService.BLL;
using CommentService.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Platform.DAL;
using Rabbit.Platform;
using System.Text;

namespace CommentApi
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var services = builder.Services;

            configuration.AddJsonFile("Properties/secretsSettings.json");
            configuration.AddJsonFile("Properties/rabbitSettings.json");

            services.Configure<RedisOptions>(configuration.GetSection(nameof(RedisOptions)));
            services.AddSingleton<IContextManager, ContextManager>();
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddSingleton<ISecretsSettings, SecretsSettings>();
            services.AddTransient<ICommentMainService, CommentService.BLL.CommentMainService>();
            services.AddTransient<ICommentReplyService, CommentReplyService>();
            services.AddSingleton<IRabbitSettings, RabbitSettings>();
            services.AddSingleton<IRabbitMQService, RabbitMQService>();
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<IMessageConsumer, MessageConsumer>();

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
            });

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
