using GreeterServiceApp;
using Grpc.Core;

namespace GreeterServiceApp.Services
{
    /// <summary>
    /// Класс GreeterService по сути представляет обычный класс C#. 
    /// Так, по умолчанию он имеет конструктор, который посредством dependency injection получает объект логгера и может
    /// его использовать для логирования.
    /// </summary>
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
                // отвечает за обмен сообщениями
            });
        }


       
    }
}
