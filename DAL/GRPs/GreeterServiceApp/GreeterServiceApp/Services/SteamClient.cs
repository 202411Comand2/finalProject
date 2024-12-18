using Grpc.Core;
using GrpcClientService;

namespace GreeterServiceApp.Services
{
    public class SteamClient: Messenger.MessengerBase
    {
        public override async Task<Response> ClientDataStream(IAsyncStreamReader<Request> requestStream, ServerCallContext context)
        {
            await foreach (Request request in requestStream.ReadAllAsync())
            {
                Console.WriteLine(request.Content);
            }
            Console.WriteLine("Все данные получены...");
            return new Response { Content = "все данные получены" };
        }

    }
}
