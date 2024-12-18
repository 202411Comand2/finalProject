using GreeterServiceApp;
using GreeterServiceApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);// ������ �����������


string connStr = "Server=(localdb)\\mssqllocaldb;Database=grpcdb;Trusted_Connection=True;";
// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connStr));// ��� ������� �� ����������


// ��������� ������� ��� ������ � gRPC
builder.Services.AddGrpc();



var app = builder.Build();

// ����������� ��������� HTTP-��������
// ���������� TranslatorService � ��������� �������
app.MapGrpcService<TranslatorService>();
app.MapGrpcService<GreeterService>();
app.MapGrpcService<InviterService>();
app.MapGrpcService<SteamServer>(); // ��������� �������� �������
app.MapGrpcService<SteamClient>(); // ��������� �������� �������
app.MapGrpcService<TwoSteamCS>(); // ��������� �������� ������� - �������
app.MapGrpcService<TitleService>(); // ��������� �������� ����������
app.MapGrpcService<UserApiService>(); // entity

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
