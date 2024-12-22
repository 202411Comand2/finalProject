using IdentityService.DAL.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace IdentityService.DAL
{
	public class IdentityDbContextManager : IContextManager
	{
		private readonly string _connectionString;
        public IdentityDbContextManager()
        {
			_connectionString = Secrets.UsersDatabase;
        }
        public IdentityDbContext CreateDatabaseContext()
		{
			var builder = new DbContextOptionsBuilder<IdentityDbContext>();

			var options = builder
				.UseNpgsql(_connectionString)
				.LogTo(Console.WriteLine, LogLevel.Information)
				.EnableSensitiveDataLogging()
				.Options;

			return new IdentityDbContext(options);
		}
	}
}
