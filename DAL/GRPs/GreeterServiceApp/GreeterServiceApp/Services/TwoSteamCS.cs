using Grpc.Core;
using GrpcServiceTwoSteam;

namespace GreeterServiceApp.Services
{
    public class TwoSteamCS : Messenger.MessengerBase
    {
        // сообшения для отправки
        string[] messages = { "Привет", "Норм", "...", "Нет", "пока" };

        public override async Task DataStream(IAsyncStreamReader<Request> requestStream,
            IServerStreamWriter<Response> responseStream,
            ServerCallContext context)
        {
            // считываем входящие сообщения в фоновой задаче
            var readTask = Task.Run(async () =>
            {
                await foreach (Request message in requestStream.ReadAllAsync())
                {
                    Console.WriteLine($"Client: {message.Content}");
                }
            });

            foreach (var message in messages)
            {
                // Посылаем ответ, пока клиент не закроет поток
                if (!readTask.IsCompleted)
                {
                    await responseStream.WriteAsync(new Response { Content = message });
                    Console.WriteLine(message);
                    await Task.Delay(2000);
                }
            }
            await readTask; // ожидаем завершения задачи чтения
        }
    }
}
