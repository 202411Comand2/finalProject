using Microsoft.Extensions.Configuration;

namespace Platform.DAL
{
    public class RabbitSettings : IRabbitSettings
    {
        /// <inheritdoc />
        public string RabbitHostName { get; private set; }

        public string RabbitUserName { get; private set; }

        public string RabbitUserPassword { get; private set; }

        public RabbitSettings(IConfiguration config) 
        {
            var section = config.GetRequiredSection("RabbitMQ");
            RabbitHostName = section.GetValue<string>("HostName");
            RabbitUserName = section.GetValue<string>("UserName");
            RabbitUserPassword = section.GetValue<string>("Password");
        }
    }
}
