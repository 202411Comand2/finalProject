namespace Rabbit.Platform
{
    /// <summary>
    /// Интерфейс для сообщений, отправляемых через RabbitMQ.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Уникальный Guid сообщения.
        /// </summary>
        Guid Guid { get; }

        /// <summary>
        /// Дата создания сообщения.
        /// </summary>
        DateTime CreateDate { get; }
    }
}