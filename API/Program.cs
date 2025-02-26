using API.Extensions;
using BLL.Identity;
using BLL.Identity.Abstractions;
using BLL.Products.Abstractions;
using BLL.ProductService;
using DAL;
using DAL.Abstractions;
using DAL.ConfigSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var configuration = builder.Configuration;
			var services = builder.Services;

			configuration.AddJsonFile("Properties/secretsSettings.json");

			services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
			// Add services to the container.
			services.AddSingleton<IContextManager, ContextManager>();
			services.AddSingleton<IGenericDataHasher<string>, StringHasher>();
			services.AddSingleton<IAppSettings, AppSettings>();
			services.AddSingleton<ISecretsSettings, SecretsSettings>();
			services.AddTransient<IJwtTokenProvider, JwtTokenProvider>();
			services.AddTransient<IIdentityService, IdentityService>();
			services.AddApiAuthentication(services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());
          
			services.AddTransient<IProductService, ProductService>();

            services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
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
