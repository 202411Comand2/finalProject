using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Rabbit.Platform
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IRabbitMQService _rabbitMQService;

        public MessagePublisher(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        /// <inheritdoc />
        public async Task SendMessageAsync<T>(string exchangeName, RoutingKeys routingKey, T message) where T : IMessage
        {
            var connection = await _rabbitMQService.CreateConnectionAsync();

            using var channel = await connection.CreateChannelAsync();
            
            await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);
            
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                            exchange: exchangeName,
                            routingKey: routingKey.ToString(),
                            body: body);
        }
    }
}