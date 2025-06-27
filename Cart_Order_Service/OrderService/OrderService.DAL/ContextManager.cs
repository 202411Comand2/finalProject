using OrderService.DAL.Abstractions;
using OrderService.DAL.ConfigSettings;
using Microsoft.EntityFrameworkCore;

namespace OrderService.DAL
{
    public class ContextManager : IContextManager
    {
        /// <summary>
        /// Перезаписать бд путём удаления старой бд и замена на новую
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
        public ApplicationDbContext CreateDatabaseContext()
        {
            var builder = new DbContextOptionsBuilder();
            return new ApplicationDbContext(builder.UseNpgsql(_connectionString)
                //.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging(true)
                .Options);
        }
    }
}
