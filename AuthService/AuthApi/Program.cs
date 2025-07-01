
using AuthService.BLL;
using AuthService.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Platform.DAL;
using Rabbit.Platform;
using System.Text;


namespace AuthApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            configuration.AddJsonFile("Properties/secretsSettings.json");
            configuration.AddJsonFile("Properties/rabbitSettings.json");

            var services = builder.Services;

            services.AddSingleton<IContextManager, ContextManager>();
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddSingleton<ISecretsSettings, SecretsSettings>();
            services.AddTransient<IAuthMainService, AuthMainService>();
            services.AddSingleton<IRabbitSettings, RabbitSettings>();
            services.AddSingleton<IRabbitMQService, RabbitMQService>();
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<IMessageConsumer, MessageConsumer>();


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
