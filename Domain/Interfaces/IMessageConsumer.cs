namespace Rabbit.Platform
{
    /// <summary>
    /// Интерфейс менеджера для получения сообщений из RabbitMQ.
    /// </summary>
    public interface IMessageConsumer
    {
        /// <summary>
        /// Получает и обрабатывает сообщения из RabbitMQ.
        /// </summary>
        /// <typeparam name="T">Тип сообщения.</typeparam>
        /// <param name="queue">Наименование очереди.</param>
        /// <param name="exchange">Наименование обменника сообщений.</param>
        /// <param name="routingKey">Ключ маршрутизации сообщения.</param>
        /// <param name="handler">Обработчик сообщения.</param>
        /// <returns>Задача для ассинхронного получения и обработки сообщения.</returns>
        Task ProcessMessageAsync<T>(Func<T, Task> handler, RoutingKeys routingKey, string exchange = "", string queue = "") where T : IMessage;
    }
}
