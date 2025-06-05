namespace Rabbit.Platform
{
    public interface IMessagePublisher
    {
        /// <summary>
        /// Менеджер отправки сообщения в RabbitMQ.
        /// </summary>
        /// <typeparam name="T">Тип сообщения.</typeparam>
        /// <param name="exchangeName">Имя обменника Rabbit.</param>
        /// <param name="routingKey">Ключ маршрутизации сообщения.</param>
        /// <param name="message">Сообщение для отправки.</param>
        /// <returns>Задача для асинхронной отправки сообщения в Rabbit.</returns>
        Task SendMessageAsync<T>(string exchangeName, RoutingKeys routingKey, T message) where T : IMessage;
    }
}