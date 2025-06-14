using Platform.DAL;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace Rabbit.Platform
{
    /// <summary>
    /// Менеджер для получения сообщений из RabbitMQ.
    /// </summary>
    public class MessageConsumer : IMessageConsumer
    {
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IRabbitSettings _rabbitSettings;

        public MessageConsumer(IRabbitMQService rabbitMQService,
            IRabbitSettings rabbitSettings)
        {
            _rabbitMQService = rabbitMQService;
            _rabbitSettings = rabbitSettings;
        }

        /// <inheritdoc />
        public async Task ProcessMessageAsync<T>(Func<T, Task> handler, string routingKey, string exchange = "", string queue = "") where T : IMessage
        {
            var connection = await _rabbitMQService.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            if (string.IsNullOrEmpty(exchange))
            {
                exchange = _rabbitSettings.RabbitDefaultExchangeName;
            }

            if (string.IsNullOrEmpty(queue))
            {
                queue = _rabbitSettings.RabbitDefaultQueueName;
            }

            await channel.ExchangeDeclareAsync(exchange, ExchangeType.Direct);
            await channel.QueueDeclareAsync(queue, true, false, false);
            await channel.QueueBindAsync(queue, exchange, routingKey);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<T>(json);

                if (message != null)
                {
                    await handler(message);
                }
            };

            await channel.BasicConsumeAsync(queue, true, consumer);
        }
    }
}