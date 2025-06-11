using Microsoft.Extensions.Configuration;

namespace Platform.DAL
{
    public class RabbitSettings : IRabbitSettings
    {
        /// <inheritdoc />
        public string RabbitHostName { get; private set; }

        /// <inheritdoc />
        public string RabbitUserName { get; private set; }

        /// <inheritdoc />
        public string RabbitUserPassword { get; private set; }

        /// <inheritdoc />
        public string RabbitDefaultExchangeName { get; private set; } = "amq.direct";

        /// <inheritdoc />
        public string RabbitDefaultQueueName { get; private set; } = "default.queue";

        public RabbitSettings(IConfiguration config) 
        {
            var section = config.GetRequiredSection("RabbitMQ");
            RabbitHostName = section.GetValue<string>("HostName");
            RabbitUserName = section.GetValue<string>("UserName");
            RabbitUserPassword = section.GetValue<string>("Password");
            RabbitDefaultExchangeName = section.GetValue<string>("DefaultExchangeName");
            RabbitDefaultQueueName = section.GetValue<string>("DefaultQueueName");
        }
    }
}
