namespace Platform.DAL
{
    /// <summary>
    /// Интерфейс настроек RabbitMQ.
    /// </summary>
    public interface IRabbitSettings
    {        
        /// <summary>
        /// Hост RabbitMQ.
        /// </summary>
        string RabbitHostName { get; }
        
        /// <summary>
        /// Имя пользователя для работы с RabbitMQ.
        /// </summary>
        string RabbitUserName { get; }
        
        /// <summary>
        /// Пароль пользователя для работы с RabbitMQ.
        /// </summary>
        string RabbitUserPassword { get; }
    }
}
