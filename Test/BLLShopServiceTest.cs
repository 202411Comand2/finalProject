using BLL.Identity;
using BLL.ProductService;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NpgsqlTypes;


namespace Test
{
    internal class BLLShopServiceTest
    {
        //private UserRepository _userRepository;
        private ProductService _productService;
        //private ShopRepository _shopRepository;
        //private ShopOwnerRepository _shopOwnerRepository;


        /// <summary>
        /// Для вывода в консоль данных
        /// </summary>
        /// <param name="word"></param>
        private void ConsoleLog(string word)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(word);
            Console.ForegroundColor = ConsoleColor.White;

        }

        /// <summary>
        /// Тестирование создание магазина пользователем
        /// </summary>
        /// <returns></returns>
        public BLLShopServiceTest(IContextManager cm)
        {
            _productService = new ProductService(cm);
            //_userRepository = new UserRepository(cm);
            //_shopRepository = new ShopRepository(cm);
            //_shopOwnerRepository = new ShopOwnerRepository(cm);
        }

        #region работа с магазином

        /// <summary>
        /// Создание Владельца магазина
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CreateShop()
        {
            //User user = await _userRepository.Get(1);
            //if (user == null)
            //{
            //    return false;
            //}
            Random rand = new Random();
            string message = await _productService.CreateShop($"Название магазина{rand.Next(0, 10_000)}");
            ConsoleLog(message);
            // для теста получаем пользователя предполагаю, что пользователь будет приходить из вне
            //await addShop.CreateShopUser();
            return true;
        }

        /// <summary>
        /// Удаление магазина
        /// </summary>
        /// <returns></returns>
        public async Task testDeleteShop()
        {
            List<string> l = new List<string>() { "we", "12", "Название магазина" };
            foreach (string s in l)
            {
                string message = await _productService.DeleteShop(s);
                ConsoleLog(message);

            }
        }


        /// <summary>
        /// Изменить имя магазина
        /// </summary>
        /// <returns></returns>
        public async Task TestUpdateNameShop()
        {
            List<int> idListShop = new List<int>() { 1, 2, 3 };
            List<string> newName = new List<string>() { "we", "_-_", "1221212" };


            for (int i = 0; i < idListShop.Count; i++)
            {
                string message = await _productService.UpdateShopName(idListShop[i], newName[i]);
                ConsoleLog(message + $"\n idListShop {idListShop[i]} {newName[i]}");
            }

            string message1 = await _productService.UpdateShopName("we", "Новое имя");
            // тут меняем на новое имя через строку
            ConsoleLog(message1 + $"\n  we  Новое имя");
        }
        #endregion

        #region Работа с кластером

        /// <summary>
        /// Создание элемента классификатора 
        /// </summary>
        /// <returns></returns>
        public async Task TestAddCluster()
        {
            string message = await _productService.AddNewCluster("кластер");
            // корневой элемент
            ConsoleLog(message);

            for (int i = 0; i < 10; i++)
            {

                message = await _productService.AddNewCluster($"кластер1{i}");
                // Дочерний элемент
                ConsoleLog(message);
            }
            for (int i = 0; i < 10; i++)
            {

                message = await _productService.AddNewCluster($"кластер_Дочерний{i}", "кластер1");
                // Дочерний элемент
                ConsoleLog(message);
            }
            message = await _productService.AddNewCluster("кластер1", 1);
            // Дочерний элемент
            ConsoleLog(message);

            message = await _productService.AddNewCluster("кластер", 1);
            // Дочерний элемент
            ConsoleLog(message);

            message = await _productService.AddNewCluster("кластер тестирование string", "кластер");
            // Дочерний элемент
            ConsoleLog(message);
        }


        /// <summary>
        /// Обновление кластера
        /// </summary>
        /// <returns></returns>
        public async Task TestUpdateCluster()
        {
            string message = await _productService.UpdateNameCluster(1, "кластер");
            ConsoleLog(message);
            message = await _productService.UpdateNameCluster(1, "test");
            ConsoleLog(message);
            message = await _productService.UpdateNameCluster(10, "test");
            ConsoleLog(message);
            message = await _productService.UpdateNameCluster("test", "UpdateNameCluster");
            ConsoleLog(message);
        }

        /// <summary>
        /// Удаление кластера
        /// </summary>
        /// <returns></returns>
        public async Task TestDeleteCluster()
        {
            string message = await _productService.DeleteCluster(1);
            ConsoleLog(message);
            message = await _productService.DeleteCluster("кластер тестирование string");
            ConsoleLog(message);
            message = await _productService.DeleteCluster(100000);
            ConsoleLog(message);
            message = await _productService.DeleteCluster("Упадёт ли кластер или нет");
            ConsoleLog(message);
        }

        /// <summary>
        /// Обновить позицию кластера в классификаторе
        /// </summary>
        /// <returns></returns>
        public async Task TestUpdatePositionCluster()
        {
            string message = await _productService.UpdatePositionCluster(2, 10);
            ConsoleLog(message);

        }

        /// <summary>
        /// Получить кластеры по определённым условиям
        /// </summary>
        /// <returns></returns>
        public async Task TestGetCluster()
        {
            Console.WriteLine("\nПолучить все элементы кластеров:");
            foreach (var item in await _productService.GetAllElementsCluster())
            {
                Console.WriteLine($"{item.Name} {item.ParentId}");
            }

            Console.WriteLine("\nПолучить только корневые элементы кластера:");
            foreach (var item in await _productService.GetRootElementsCluster())
            {
                Console.WriteLine($"{item.Name} {item.ParentId}");
            }
            Console.WriteLine("\nПолучить дочерние элементы кластера:");
            foreach (var item in await _productService.GetChildrenElementsCluster(2))
            {
                Console.WriteLine($"{item.Name} {item.ParentId}");
            }
        }
        #endregion

        #region работа с продуктами просто для тестирования других сервисов
        public async Task TestCreateProduct()
        {
            var  message = await _productService.AddProduct(1, 2);
            ConsoleLog(message);
        }
        #endregion

        #region Работа с отзывами (+ рейтинг товара, так как это внутреняя вещь отзывов)

        /// <summary>
        /// Добавить комментарий
        /// </summary>
        /// <returns></returns>
        public async Task TestAddComment() 
        {
            string message = await _productService.AddNewComment(1,1,1,"Комментарий", 4.3m);
            ConsoleLog(message);
        }

        /// <summary>
        /// Обновить комментарий
        /// </summary>
        /// <returns></returns>
        public async Task TestUpdateComment() 
        {
            string message = await _productService.UpdateComment( 1, "Комментарий обновлённый", 4.3m);
            ConsoleLog(message);
            message = await _productService.UpdateComment(1, "Комментарий обновлённый ошибка", 4.3m);
            ConsoleLog(message);
        }
        /// <summary>
        /// Удаление(скрыть) комментария пользователя
        /// </summary>
        /// <returns></returns>
        public async Task TestDeleteComment()
        {
            string message = await _productService.DeleteComment(1);
            ConsoleLog(message);
            message = await _productService.DeleteComment(1);
            ConsoleLog(message);
        }


        /// <summary>
        /// Добавить  ответ на комментарий пользователя со стороны магазина
        /// </summary>
        /// <returns></returns>
        public async Task TestAddCommentReply()
        {
            string message = await _productService.AddNewOrUpdateCommentReplyIdCommentUser(1,"Ответный комментарий");
            ConsoleLog(message);

            message = await _productService.AddNewOrUpdateCommentReplyIdCommentReply(1, "Ответный комментарий со стороны Reply");
            ConsoleLog(message);
        }

        /// <summary>
        /// Удалить(скрыть) ответы на комментарии владельцев товара
        /// </summary>
        /// <returns></returns>
        public async Task TestDeleteCommentReply()
        {
            string message = await _productService.AddNewOrUpdateCommentReplyIdCommentUser(1, "Ответный комментарий");
            ConsoleLog(message);

            message = await _productService.AddNewOrUpdateCommentReplyIdCommentReply(1, "Ответный комментарий со стороны Reply");
            ConsoleLog(message);
        }

        /// <summary>
        /// Получить все комментарии по товару
        /// </summary>
        /// <returns></returns>
        public async Task GetAllComment()
        {
            foreach (var item in await _productService.GetCommentProduct(1))
            {
                ConsoleLog($"{item.Text} {item.IsDeleted}" );
            }

        }
        #endregion


       
    }

}
