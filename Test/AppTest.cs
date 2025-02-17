using DAL;
using DAL.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class AppTest
    {
        private readonly IBLLShopServiceTest _bLLShopServiceTest;
        private readonly IBLLIdentityServiceTests _bLLIdentityServiceTest;

        public AppTest(IBLLShopServiceTest bLLShopServiceTest, IBLLIdentityServiceTests bLLIdentityServiceTest)
        {
            _bLLShopServiceTest = bLLShopServiceTest;
            _bLLIdentityServiceTest = bLLIdentityServiceTest;
        }

        public async Task ExecuteAsync()
        {
            await TestCreateUser();
            await TestShop();
            await TestCluster();
            await TestProduct();
            await TestComment();
            await TestFavoriteProduct();
        }

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <returns></returns>
        private async Task TestCreateUser()
        {
            await _bLLIdentityServiceTest.CreateGuestTokenNotNullTest();
            await _bLLIdentityServiceTest.CreateNewUserByPhoneTest();
            await _bLLIdentityServiceTest.CreateNewUserByEmailTest();
            await _bLLIdentityServiceTest.LogInByPhoneTest();
            await _bLLIdentityServiceTest.LogInByEmailTest();
            await _bLLIdentityServiceTest.CreateNewUserWithSamePhoneTest();
            await _bLLIdentityServiceTest.CreateNewUserWithSameEmailTest();
        }


        /// <summary>
        /// Тестирование создание продукта
        /// </summary>
        /// <returns></returns>
        private async Task TestProduct()
        {
            await _bLLShopServiceTest.TestCreateProduct();

        }

        /// <summary>
        /// Класс для шалости с магазином
        /// </summary>
        private async Task TestShop()
        {
            var s = await _bLLShopServiceTest.CreateShop();
            //Создать магазин
            await _bLLShopServiceTest.TestUpdateNameShop();
            // обновить название магазина
            await _bLLShopServiceTest.TestDeleteShop();
            // удалить магазин
        }

        /// <summary>
        /// Тестирование кластера
        /// </summary>
        /// <returns></returns>
        private async Task TestCluster()
        {
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
        private async Task TestComment()
        {
            await _bLLShopServiceTest.TestAddComment();
            // добавление отзыва
            await _bLLShopServiceTest.TestUpdateComment();
            //// Обновлённый комментарий
            await _bLLShopServiceTest.TestDeleteComment();
            //// удаление комментария
            await _bLLShopServiceTest.GetAllComment();
            // получить всё комментарии по товару
            await _bLLShopServiceTest.TestAddCommentReply();
            ////ответные комментарии владельца товара
            await _bLLShopServiceTest.TestDeleteCommentReply();
            ////удаление ответов на комментарий

        }
      
        /// <summary>
        /// тестирование избранных позиций
        /// </summary>
        /// <returns></returns>
        private async Task TestFavoriteProduct()
        {
            await _bLLShopServiceTest.TestAddFavoriteProduct();
            ////Добавить позицию в избранное
            await _bLLShopServiceTest.TestDeleteFavoriteProduct();
            ////Удалить позицию из избранного
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

