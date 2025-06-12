namespace Rabbit.Platform
{
    /// <summary>
    /// Интерфейс менеджера для отправки сообщений в RabbitMQ.
    /// </summary>
    public interface IMessagePublisher
    {
        /// <summary>
        /// Отправляет сообщения в RabbitMQ.
        /// </summary>
        /// <typeparam name="T">Тип сообщения.</typeparam>
        /// <param name="exchangeName">Имя обменника Rabbit.</param>
        /// <param name="routingKey">Ключ маршрутизации сообщения.</param>
        /// <param name="message">Сообщение для отправки.</param>
        /// <returns>Задача для асинхронной отправки сообщения в Rabbit.</returns>
        Task SendMessageAsync<T>(T message, RoutingKeys routingKey, string exchangeName = "") where T : IMessage;
    }
}