using Microsoft.EntityFrameworkCore;

namespace ClusterService.DAL
{
	public class ContextManager : IContextManager
	{
        /// <summary>
        /// Перезаписать бд,путём удаления старой бд и замена на новую
        /// </summary>
        public bool flagCreateBD = false;

		private readonly string _connectionString;

		/// <summary>
		/// Строка подключения
		/// </summary>
		public ContextManager(ISecretsSettings secretsSettings)
		{
			_connectionString = secretsSettings.ConnectionString;
		}

		/// <summary>
		/// Создание контекста подключения
		/// </summary>
		/// <returns></returns>
		public ApplicationDbContext CreateDatabaseContext()
		{
			var builder = new DbContextOptionsBuilder();
			return new ApplicationDbContext(builder.UseNpgsql(_connectionString)
				//.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
				.EnableSensitiveDataLogging(true)
				.Options
				);
		}	
    }
}
