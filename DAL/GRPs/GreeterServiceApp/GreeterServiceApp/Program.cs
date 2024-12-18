using GreeterServiceApp;
using GreeterServiceApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);// строка подключения


string connStr = "Server=(localdb)\\mssqllocaldb;Database=grpcdb;Trusted_Connection=True;";
// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connStr));// эта редиска не подсказала


// добавляем сервисы для работы с gRPC
builder.Services.AddGrpc();



var app = builder.Build();

// настраиваем обработку HTTP-запросов
// встраиваем TranslatorService в обработку запроса
app.MapGrpcService<TranslatorService>();
app.MapGrpcService<GreeterService>();
app.MapGrpcService<InviterService>();
app.MapGrpcService<SteamServer>(); // потоковая передача сервера
app.MapGrpcService<SteamClient>(); // потоковая передача клиента
app.MapGrpcService<TwoSteamCS>(); // потоковая передача клиента - сервера
app.MapGrpcService<TitleService>(); // потоковая передача заголовков
app.MapGrpcService<UserApiService>(); // entity

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
