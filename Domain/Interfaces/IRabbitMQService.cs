using RabbitMQ.Client;

namespace Rabbit.Platform
{
    /// <summary>
    /// Интерфейс для работы с RabbitMQ.
    /// </summary>
    public interface IRabbitMQService : IDisposable
    {
        /// <summary>
        /// Создает соединение с RabbitMQ.
        /// </summary>
        /// <returns>Соединение с RabbitMQ</returns>
        Task<IConnection> CreateConnectionAsync();

    }
}
