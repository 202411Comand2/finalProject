using IdentityService.BLL.Abstractions;
using IdentityService.BLL.Abstractions.Utilities;
using IdentityService.BLL.Utilities;
using IdentityService.DAL;
using IdentityService.DAL.Abstractions;

namespace IdentityService.API
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var services = builder.Services;
			var configuration = builder.Configuration;

			services.AddSingleton<IContextManager, ContextManager>();
			services.AddSingleton<IHasher, BcryptDataHasher>();
			services.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();
			services.AddTransient<IIdentityService, BLL.IdentityService>();
			services.AddControllers();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			app.MapControllers();
			app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
			
			app.Run();
		}
	}
}