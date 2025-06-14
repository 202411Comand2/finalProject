using Microsoft.Extensions.Configuration;
using Platform.DAL;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Rabbit.Platform
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IRabbitSettings _rabbitSettings;

        public MessagePublisher(IRabbitMQService rabbitMQService, 
            IRabbitSettings rabbitSettings)
        {
            _rabbitMQService = rabbitMQService;
            _rabbitSettings = rabbitSettings;
        }

        /// <inheritdoc />
        public async Task SendMessageAsync<T>(T message, string routingKey, string exchangeName = "") where T : IMessage
        {
            using var connection = await _rabbitMQService.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            if (string.IsNullOrEmpty(exchangeName))
            {
                exchangeName = _rabbitSettings.RabbitDefaultExchangeName;
            }

            await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchangeName, routingKey, body);
        }
    }
}