using DAL;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using DAL.ConfigSettings;
using DAL.Abstractions;

namespace Test
{
    public delegate void Log(string message);
    internal class Program
    {
        static async Task Main(string[] args)
        {
            #region configure application
            var config = CreateConfiguration(args);
            var serviceProvider = CreateServiceCollection(config).BuildServiceProvider();
            #endregion

            #region создание пользователя (Глеб)
            //_contextManager = new ContextManager();
            // _bllIdentityTests = new BLLIdentyServiceTests(_contextManager, Log);
            // await _bllIdentityTests.CreateGuestTokenTest();
            //  await _bllIdentityTests.CreateNewUserTest();
            #endregion
            await serviceProvider.GetService<AppTest>().ExecuteAsync();
        }


        private static IConfiguration CreateConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Configs")
                    .AddJsonFile("appSettings.json", false, false)
                    .AddJsonFile("secretsSettings.json", false, false)
                    .AddEnvironmentVariables()
                    .Build();               
        }

        private static IServiceCollection CreateServiceCollection(IConfiguration config)
        {
            return new ServiceCollection()
                        .AddSingleton(config)
                        .AddSingleton<ISecretsSettings, SecretsSettings>()
                        .AddSingleton<IContextManager, ContextManager>()
                        .AddTransient<IBLLShopServiceTest, BLLShopServiceTest>()
                        .AddTransient<IBLLIdentityServiceTests, BLLIdentityServiceTests>()
                        .AddTransient<AppTest>();
        }
    }
}
