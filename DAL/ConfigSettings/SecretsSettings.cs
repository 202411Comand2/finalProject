using Microsoft.Extensions.Configuration;

namespace DAL.ConfigSettings
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
            var section = config.GetRequiredSection("SecretsSettings");
            ConnectionString = section.GetValue<string>("ConnectionString");
        }
    }
}
