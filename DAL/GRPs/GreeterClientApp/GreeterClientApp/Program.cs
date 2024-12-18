
/*
пакеты
Grpc.Net.Client
Google.Protobuf
Grpc.Tools

 */
using Grpc.Net.Client;
using GreeterClientApp;
using GreeterServiceApp;
using GrpcServiceApp;
using GrpcServiceDateTimeApp;
using GrpcSteamService;
using Grpc.Core;
using GrpcServiceEntity;

namespace GreeterClientApp
{
    internal class Program
    {

        static private void Copy()
        {
            string path = @"C:\Users\Ilya\source\repos\GreeterServiceApp\GreeterServiceApp\Protos\greet.proto";
            string path2 = @"C:\Users\Ilya\source\repos\GreeterClientApp\GreeterClientApp\Protos\greet.proto";
            File.Copy(path, path2, true);

            string path_ = @"C:\Users\Ilya\source\repos\GreeterServiceApp\GreeterServiceApp\Protos\Translator.proto";
            string path2_ = @"C:\Users\Ilya\source\repos\GreeterClientApp\GreeterClientApp\Protos\Translator.proto";
            File.Copy(path_, path2_, true);

            string path__ = @"C:\Users\Ilya\source\repos\GreeterServiceApp\GreeterServiceApp\Protos\DateTimeMiting.proto";
            string path2__ = @"C:\Users\Ilya\source\repos\GreeterClientApp\GreeterClientApp\Protos\DateTimeMiting.proto";
            File.Copy(path__, path2__, true);

            string path___ = @"C:\Users\Ilya\source\repos\GreeterServiceApp\GreeterServiceApp\Protos\SteamServer.proto";
            string path2___ = @"C:\Users\Ilya\source\repos\GreeterClientApp\GreeterClientApp\Protos\SteamServer.proto";
            File.Copy(path___, path2___, true);

            string path____ = @"C:\Users\Ilya\source\repos\GreeterServiceApp\GreeterServiceApp\Protos\SteamClient.proto";
            string path2____ = @"C:\Users\Ilya\source\repos\GreeterClientApp\GreeterClientApp\Protos\SteamClient.proto";
            File.Copy(path____, path2____, true);
           
            
            string path_____ = @"C:\Users\Ilya\source\repos\GreeterServiceApp\GreeterServiceApp\Protos\TwoSteamClientSercer.proto";
            string path2_____ = @"C:\Users\Ilya\source\repos\GreeterClientApp\GreeterClientApp\Protos\TwoSteamClientSercer.proto";
            File.Copy(path_____, path2_____, true);
            

            path_____ = @"C:\Users\Ilya\source\repos\GreeterServiceApp\GreeterServiceApp\Protos\Title.proto";
            path2_____ = @"C:\Users\Ilya\source\repos\GreeterClientApp\GreeterClientApp\Protos\Title.proto";
            File.Copy(path_____, path2_____, true);

            path_____ = @"C:\Users\Ilya\source\repos\GreeterServiceApp\GreeterServiceApp\Protos\crud.proto";
            path2_____ = @"C:\Users\Ilya\source\repos\GreeterClientApp\GreeterClientApp\Protos\crud.proto";
            File.Copy(path_____, path2_____, true);
        }

        static async Task Main(string[] args)
        {


            Copy();

            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:7133");
            // создаем клиент
            var client = new Greeter.GreeterClient(channel);
            //Console.Write("Введите имя: ");
            //string? name = Console.ReadLine();
            //// обмениваемся сообщениями с сервером
            //var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "АБДЖ" });
            Console.WriteLine($"Ответ сервера: {reply.Message}");

            //await twoService();
            //await DateTimeService();
            //await ServerSteam();
            //await Serverclient();
            //await ServerclientTwoSteam();
            //await ServicTitle();
            await CRUD();
            Console.ReadKey();
        }

        static async Task twoService()
        {
            // список слов для получения перевода
            var words = new List<string>() { "red", "yellow", "green" };
            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:7133");

            // создаем клиент
            var client = new Translator.TranslatorClient(channel);

            // отправляем каждое слово сервису для получения перевода
            foreach (var word in words)
            {
                // формируем сообщение для отправки
                GrpcServiceApp.Request request = new GrpcServiceApp.Request { Word = word };
                // отправляем сообщение и получаем ответ
                GrpcServiceApp.Response response = await client.TranslateAsync(request);
                // выводим слово и его перевод
                Console.WriteLine($"{response.Word} : {response.Translation}");
            }
        }


        /// <summary>
        /// третий сервис с датой
        /// </summary>
        /// <returns></returns>
        static async Task DateTimeService()
        {
            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:7133");

            // создаем клиент
            var client = new Inviter.InviterClient(channel);

            // посылаем имя и получаем приглашение на мероприятие
            var response = await client.InviteAsync(new GrpcServiceDateTimeApp.Request { Name = "Tom" });
            var eventInvitation = response.Invitation;
            var eventDateTime = response.Start.ToDateTime();
            var eventDuration = response.Duration.ToTimeSpan();
            // выводим данные на консоль
            Console.WriteLine(eventInvitation);
            Console.WriteLine($"Начало: {eventDateTime.ToString("dd.MM HH:mm")}   Длительность: {eventDuration.TotalHours} часа");
        }

        /// <summary>
        /// Потоковая передача сервера
        /// </summary>
        /// <returns></returns>
        static async Task ServerSteam()
        {
            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:7133");

            // создаем клиент
            var client = new Messenger.MessengerClient(channel);

            // посылаем пустое сообщение и получаем набор сообщений
            var serverData = client.ServerDataStream(new GrpcSteamService.Request());

            // получаем поток сервера
            var responseStream = serverData.ResponseStream;
            // с помощью итераторов извлекаем каждое сообщение из потока
            while (await responseStream.MoveNext(new CancellationToken()))
            {

                GrpcSteamService.Response response = responseStream.Current;
                Console.WriteLine(response.Content);
            }
        }


        /// <summary>
        /// Потоковая передача сервера
        /// </summary>
        /// <returns></returns>
        static async Task Serverclient()
        {
            string[] messages = { "Привет", "Как дела?", "Че молчишь?", "Ты че, спишь?", "Ну пока" };


            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:7133");

            // создаем клиент
            var client = new GrpcClientService.Messenger.MessengerClient(channel);

            var call = client.ClientDataStream();

            // посылаем каждое сообщение
            foreach (var message in messages)
            {
                await call.RequestStream.WriteAsync(new GrpcClientService.Request { Content = message });
            }
            // завершаем отправку сообшений в потоке
            await call.RequestStream.CompleteAsync();
            // получаем ответ сервера
            GrpcClientService.Response response = await call.ResponseAsync;
            Console.WriteLine($"Ответ сервера: {response.Content}");
        }

        /// <summary>
        /// отсылка сообщение клиент сервер
        /// </summary>
        /// <returns></returns>
        static async Task ServerclientTwoSteam()
        {
            // данные для отправки
            string[] messages = { "Привет", "Как дела?", "Че молчишь?", "Ты че, спишь?", "Ну пока" };

            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:7133");

            // создаем клиент
            var client = new GrpcServiceTwoSteam.Messenger.MessengerClient(channel);

            // получаем объект AsyncDuplexStreamingCall
            var call = client.DataStream();

            var readTask = Task.Run(async () =>
            {
                await foreach (var response in call.ResponseStream.ReadAllAsync())
                {
                    Console.WriteLine($"Server: {response.Content}");
                }
            });
            foreach (var message in messages)
            {
                await call.RequestStream.WriteAsync(new GrpcServiceTwoSteam.Request { Content = message });
                Console.WriteLine(message);
                await Task.Delay(2000);
            }

            // завершаем отправку сообщений на сервер
            await call.RequestStream.CompleteAsync();
            await readTask;
        }



        /// <summary>
        /// Отправка получение заголовков
        /// </summary>
        /// <returns></returns>
        static async Task ServicTitle()
        {

            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:7133");
          
            var client = new GrpcServiceTitle.Messenger.MessengerClient(channel);

            // формируем отправляемые заголовки
            Metadata requestHeaders = new Metadata();
            // добавляем один заголовок
            requestHeaders.Add("username", "Tom");
            // отправляем сообщение серверу
            using var call =  client.SendMessageAsync(new GrpcServiceTitle.Request(), requestHeaders);

            // получаем ответ
            GrpcServiceTitle.Response response = await call.ResponseAsync;
            Console.WriteLine($"Response: {response.Content}");
            // получаем все заголовки и выводим их на консоль
            Metadata responseHeaders = await call.ResponseHeadersAsync;
            foreach (var header in responseHeaders)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");
            }
        }



        /// <summary>
        /// Grud операции
        /// </summary>
        /// <returns></returns>
        static async Task CRUD()
        {

            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:7133");

            // создаем клиент
            var client = new UserService.UserServiceClient(channel);

            // получение списка объектов
            ListReply users = await client.ListUsersAsync(new Google.Protobuf.WellKnownTypes.Empty());

            foreach (var user in users.Users)
            {
                Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
            }

            #region Получение 1-го пользователя
            Console.WriteLine($"Получение 1-го пользователя");
            try
            {
                // получение одного объекта по id = 2
                UserReply user = await client.GetUserAsync(new GetUserRequest { Id = 2 });
                Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Status.Detail);    // получаем статус ответа
            }
            #endregion

            #region добовление пользователя
            Console.WriteLine($"добовление пользователя");

            // добавление одного объекта
            UserReply userAdd = await client.CreateUserAsync(new CreateUserRequest { Name = "Alice", Age = 32 });
            Console.WriteLine($"{userAdd.Id}. {userAdd.Name} - {userAdd.Age}");
            #endregion

            #region обновление пользователя
            Console.WriteLine($"обновление пользователя");
            try
            {
                //обновление одного объекта - изменим имя у объекта с id = 1 на Tomas
                UserReply user = await client.UpdateUserAsync(new UpdateUserRequest { Id = 1, Name = "Tomas", Age = 38 });
                Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Status.Detail);
            }
            #endregion


            #region удаление пользователя

            try
            {
                Console.WriteLine($"удаление пользователя");

                // удаление объекта с id = 2
                UserReply user = await client.DeleteUserAsync(new DeleteUserRequest { Id = 2 });
                Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
            }
            catch (RpcException ex)
            {
                Console.WriteLine(ex.Status.Detail);
            }
            #endregion
        }

    }
}
