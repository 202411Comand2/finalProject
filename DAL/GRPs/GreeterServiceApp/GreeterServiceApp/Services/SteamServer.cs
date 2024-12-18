using Grpc.Core;
using GrpcSteamService;  // пространство имен сервиса MessengerService

public class SteamServer : Messenger.MessengerBase
{
    string[] messages = { "Привет", "Как дела?", "Че молчишь?", "Ты че, спишь?", "Ну пока" };
    public override async Task ServerDataStream(Request request,
        IServerStreamWriter<Response> responseStream,
        ServerCallContext context)
    {
        foreach (var message in messages)
        {
            await responseStream.WriteAsync(new Response { Content = message });
            // для имитации работы делаем задержку в 1 секунду
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}
