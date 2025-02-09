using Domain.Entities;
using DAL.Repositories;
using DAL;

namespace Test
{
    public delegate void Log(string message);
    internal class Program
    {
        private static ContextManager _contextManager;
        private static BLLIdentyServiceTests _bllIdentityTests;
        private static BLLShopServiceTest _bLLShopServiceTest;

        static async Task Main(string[] args)
        {

            #region создание пользователя (Глеб)
            //_contextManager = new ContextManager();
            // _bllIdentityTests = new BLLIdentyServiceTests(_contextManager, Log);
            // await _bllIdentityTests.CreateGuestTokenTest();
            //  await _bllIdentityTests.CreateNewUserTest();
            #endregion
            //       await TestShop();
            //     await TestCluster();
            await TestAddProduct();
        }




        /// <summary>
        /// Класс для шалости с магазином
        /// </summary>
        private static async Task TestShop()
        {
            _contextManager = new ContextManager();
            _bLLShopServiceTest = new BLLShopServiceTest(_contextManager);

            var s = await _bLLShopServiceTest.CreateShop();
            //Создать магазин
            await _bLLShopServiceTest.TestUpdateNameShop();
            // обновить название магазина
            await _bLLShopServiceTest.testDeleteShop();
            // удалить магазин
        }
        private static async Task TestCluster() 
        {
            _contextManager = new ContextManager();
            _bLLShopServiceTest = new BLLShopServiceTest(_contextManager);
            await _bLLShopServiceTest.TestAddCluster();
            // добовление кластеров
            await _bLLShopServiceTest.TestUpdateCluster();
            // обновление название у кластера
            await _bLLShopServiceTest.TestDeleteCluster();
            // удаление кластера
            await _bLLShopServiceTest.TestUpdatePositionCluster();
            //Обновление позиции кластера
            await _bLLShopServiceTest.TestGetCluster();
            //получить кластеры для построение иерархии
        }


        public static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        //static async public Task AddNewUser()
        //{
        //        Cart cart = new Cart()
        //        {
        //        };

        //        AccessToken authToken = new AccessToken()
        //        {
        //            //TokenId = new Guid(),
        //            DeviceName = "phone",
        //            ExpireDate = DateTime.UtcNow.AddDays(1),
        //        };

        //        Random random = new Random();
        //        User person = new User()
        //        {
        //            Name = $"Oleg{random.Next(0, 1000)}",
        //            //Password = "1234Pasword",
        //            Phone = "71234567899",
        //            Email = "testEmail@Yndex.ru",
        //            TelegramId = 0,
        //            //AccessToken = authToken,

        //        };
        //        //await UserRepository.Add(person);

        //}
    }
}
