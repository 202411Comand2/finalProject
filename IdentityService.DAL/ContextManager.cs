using IdentityService.DAL.Abstractions;
using IdentityService.DAL.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace IdentityService.DAL
{
	public class ContextManager : IContextManager
	{
		private readonly string _connectionString;
        public ContextManager(IOptions<DbOptions> options)
        {
			if (options.Value.ConnectionSring == null || options.Value.ConnectionSring == string.Empty)
				throw new ArgumentNullException(nameof(options.Value.ConnectionSring), "Connection string can't be null or empty");
			else
			{
				_connectionString = options.Value.ConnectionSring;
			}
        }
        public ApplicationDbContext CreateDatabaseContext()
		{
			var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

			var options = builder
				.UseNpgsql(_connectionString)
				.LogTo(Console.WriteLine, LogLevel.Information)
				.EnableSensitiveDataLogging()
				.Options;

			return new ApplicationDbContext(options);
		}
	}
}
