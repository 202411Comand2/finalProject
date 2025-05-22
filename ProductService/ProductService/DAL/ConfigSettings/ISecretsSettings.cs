namespace DAL.ConfigSettings
{
    /// <summary>
    /// Интерфейс конфиденциальных настроек приложения
    /// </summary>
    public interface ISecretsSettings
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        string ConnectionString { get; }
    }
}
