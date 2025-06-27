using RabbitMQ.Client;
using Platform.DAL;

namespace Rabbit.Platform
{
    /// <summary>
    /// Базоввый сервис для работы с RabbitMQ.
    /// </summary>
    public class RabbitMQService : IRabbitMQService
    {
        // Хост RabbitMQ.
        private readonly string _hostname = "host.docker.internal";
        // Пользователь для работы с RabbitMQ.
        private readonly string _username = "admin";
        // Пароль поьзователя для работы с RabbitMQ.
        private readonly string _password = "secret";
        // Соединение с RabbitMQ.
        private IConnection? _connection;

        public RabbitMQService(IRabbitSettings appSettings)
        {
            _hostname = "host.docker.internal";//appSettings.RabbitHostName;
            _username = "admin";// appSettings.RabbitUserName;
            _password = "secret";//appSettings.RabbitUserPassword;
        }

        /// <inheritdoc />
        public async Task<IConnection> CreateConnectionAsync()
        {
            if (_connection != null && _connection.IsOpen)
            {
                return _connection;
            }
            
            var factory = new ConnectionFactory()
            {
                HostName = "host.docker.internal",
                UserName = "admin",
                Password = "secret"
            };

            _connection = await factory.CreateConnectionAsync();
            return _connection;
        }

        /// <inheritdoc />
        public async void Dispose()
        {
            if (_connection != null && _connection.IsOpen)
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
            }
        }
    }
}