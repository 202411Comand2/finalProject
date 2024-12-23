using DAL.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
	public class ContextManager : IContextManager
	{
		private readonly string _connectionString;

		public ContextManager()
		{
			_connectionString = Secrets.Server1;
		}

		public ApplicationDbContext CreateDatabaseContext()
		{
			var builder = new DbContextOptionsBuilder();
			return new ApplicationDbContext(builder.UseNpgsql(_connectionString)
				.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
				.EnableSensitiveDataLogging(true)
				.Options
				);
		}
	}
}
