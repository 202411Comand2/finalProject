using Grpc.Core;
using GrpcServiceTitle;

namespace GreeterServiceApp.Services
{
    public class TitleService:Messenger.MessengerBase
    {
        public override async Task<Response> SendMessage(Request request, ServerCallContext context)
        {
            // получаем все заголовки запроса
            foreach (var header in context.RequestHeaders)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");    // получаем ключ и значение заголовка
            }
            // получаем один заголовок  username
            var username = context.RequestHeaders.GetValue("username");

            // формируем заголовки ответа
            Metadata responseHeaders = new Metadata();
            responseHeaders.Add("secret-code", "123445");
            // пишем заголовки в ответ
            await context.WriteResponseHeadersAsync(responseHeaders);
            // отправляем ответ
            return await Task.FromResult(new Response { Content = $"Hello {username}!" });
        }

    }
}
