using Google.Protobuf.WellKnownTypes; // пространство имен для Timestamp и Duration
using Grpc.Core;
using GrpcServiceDateTimeApp;

namespace GreeterServiceApp.Services
{
    public class InviterService : Inviter.InviterBase
    {
        public override Task<Response> Invite(Request request, ServerCallContext context)
        {
            Console.WriteLine("Времённой запрос");
            // начало мероприятия - условно следующий день 
            var eventDateTime = DateTime.UtcNow.AddDays(1);
            // длительность мероприятия - условно 2 часа
            var eventDuration = TimeSpan.FromHours(2);
            // отправляем ответ
            return Task.FromResult(new GrpcServiceDateTimeApp.Response
            {
                Invitation = $"{request.Name}, приглашаем вас посетить мероприятие",
                Start = Timestamp.FromDateTime(eventDateTime),
                Duration = Duration.FromTimeSpan(eventDuration)
            });
        }
    }
}
