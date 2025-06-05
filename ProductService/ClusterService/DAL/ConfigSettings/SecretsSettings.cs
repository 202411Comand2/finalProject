using Microsoft.Extensions.Configuration;

namespace ClusterService.DAL
{
    /// <summary>
    /// Конфиденциальные настройки приложения
    /// </summary>
    public class SecretsSettings : ISecretsSettings
    {
        /// <inheritdoc />      
        public string ConnectionString { get; private set; }

        public SecretsSettings(IConfiguration config) 
        {
            IConfigurationSection section = config.GetRequiredSection("SecretsSettings");
            ConnectionString = section.GetValue<string>("ConnectionString");

        }
    }
}
