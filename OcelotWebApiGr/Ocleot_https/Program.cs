using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;

namespace Ocleot_https
{
    //public class DebuggingHandler : DelegatingHandler
    //{
    //    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        Console.WriteLine($"Request: {request.Method} {request.RequestUri}");
    //        var response = await base.SendAsync(request, cancellationToken);
    //        Console.WriteLine($"Response: {response.StatusCode}");
    //        return response;
    //    }
    //}
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Добавление CORS политики
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            //builder.Host.ConfigureLogging((hostingContext, logging) =>
            //{
            //    logging.AddConsole();
            //    logging.AddDebug();
            //    logging.SetMinimumLevel(LogLevel.Trace);
            //});

            //builder.Services.AddOcelot()
            //    .AddDelegatingHandler<DebuggingHandler>(true);

            // Конфигурация Ocelot
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            // Настройка аутентификации
            // Настройка аутентификации JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://your-identity-server.com";
                options.Audience = "api-resource";
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    // Дополнительные параметры при необходимости
                    // ValidIssuer = "https://your-identity-server.com",
                    // ValidAudience = "api-resource",
                    // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
                };
            });

            // Добавление Ocelot
            builder.Services.AddOcelot(builder.Configuration)
                            .AddPolly();

            // Настройка HTTPS
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ConfigureHttpsDefaults(httpsOptions =>
                {
                    // Дополнительные настройки HTTPS при необходимости
                });
            });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.UseOcelot().Wait();

            app.Run();
        }
    }
}
