using Microsoft.Extensions.Configuration;
using static System.Collections.Specialized.BitVector32;

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
            //var section = config.GetRequiredSection("RabbitMQ");
            RabbitHostName = "host.docker.internal";// section.GetValue<string>("HostName");
            RabbitUserName = "admin";// section.GetValue<string>("UserName");
            RabbitUserPassword = "secret";//section.GetValue<string>("Password");
            RabbitDefaultExchangeName = "shop.exchange";// section.GetValue<string>("DefaultExchangeName");
            RabbitDefaultQueueName = "shop.queue";// section.GetValue<string>("DefaultQueueName");
        }
    }
}
