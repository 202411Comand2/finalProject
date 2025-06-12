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
        private readonly string _hostname;
        // Пользователь для работы с RabbitMQ.
        private readonly string _username;
        // Пароль поьзователя для работы с RabbitMQ.
        private readonly string _password;
        // Соединение с RabbitMQ.
        private IConnection? _connection;

        public RabbitMQService(IRabbitSettings appSettings)
        {
            _hostname = appSettings.RabbitHostName;
            _username = appSettings.RabbitUserName;
            _password = appSettings.RabbitUserPassword;
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
                HostName = _hostname,
                UserName = _username,
                Password = _password
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