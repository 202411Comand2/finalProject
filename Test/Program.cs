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

            #endregion
            await TestCreateUser();
            await TestShop();
            await TestCluster();
            await TestProduct();
            await TestComment();
        }
        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <returns></returns>
        private static async Task TestCreateUser() 
        {
            var bllTests = new BLLIdentyServiceTests();
            await bllTests.CreateGuestTokenNotNullTest();
            await bllTests.CreateNewUserByPhoneTest();
            await bllTests.CreateNewUserByEmailTest();
            await bllTests.LogInByPhoneTest();
            await bllTests.LogInByEmailTest();
            await bllTests.CreateNewUserWithSamePhoneTest();
            await bllTests.CreateNewUserWithSameEmailTest();
        }


        /// <summary>
        /// Тестирование создание продукта
        /// </summary>
        /// <returns></returns>
        private static async Task TestProduct() 
        {
            _contextManager = new ContextManager();
            _bLLShopServiceTest = new BLLShopServiceTest(_contextManager);
             await _bLLShopServiceTest.TestCreateProduct();

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
       
        /// <summary>
        /// Тестирование кластера
        /// </summary>
        /// <returns></returns>
        private static async Task TestCluster() 
        {
            _contextManager = new ContextManager();
            _bLLShopServiceTest = new BLLShopServiceTest(_contextManager);
            await _bLLShopServiceTest.TestAddCluster();
            // добавление кластеров
            await _bLLShopServiceTest.TestUpdateCluster();
            // обновление название у кластера
            await _bLLShopServiceTest.TestDeleteCluster();
            // удаление кластера
            await _bLLShopServiceTest.TestUpdatePositionCluster();
            //Обновление позиции кластера
            await _bLLShopServiceTest.TestGetCluster();
            //получить кластеры для построение иерархии
        }

        /// <summary>
        /// Тестирование комментариев
        /// </summary>
        /// <returns></returns>
        private static async Task TestComment() 
        {
            _contextManager = new ContextManager();
            _bLLShopServiceTest = new BLLShopServiceTest(_contextManager);
            await _bLLShopServiceTest.TestAddComment();
            // добавление отзыва
            await _bLLShopServiceTest.TestUpdateComment();
            // Обновлённый комментарий
            await _bLLShopServiceTest.TestDeleteComment();
            // удаление комментария
            await _bLLShopServiceTest.TestAddCommentReply();
            //ответные комментарии владельца товара
            await _bLLShopServiceTest.TestDeleteCommentReply();
            //удаление ответов на комментарий
            await _bLLShopServiceTest.GetAllComment();
            // получить всё комментарии по товару
        }

        ///// <summary>
        ///// Тестирование рейтинга
        ///// </summary>
        ///// <returns></returns>
        //private static async Task TestRating() 
        //{
        
        //}

        public static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
      
    }
}
