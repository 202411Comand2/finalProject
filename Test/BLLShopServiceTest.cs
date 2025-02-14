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
using BLL.Product;
using BLL.Abstractions;


namespace Test
{
    internal class BLLShopServiceTest : IBLLShopServiceTest
    {
        //private UserRepository _userRepository;
        private ProductService _productService;
        private ShopService _shopService; // Для создания магазина 
        //private ShopRepository _shopRepository;
        //private ShopOwnerRepository _shopOwnerRepository;


        /// <summary>
        /// Для вывода в консоль данных (Зелёный цвет)
        /// </summary>
        /// <param name="word">Текст, который нужно отразить</param>
        private void ConsoleLogGreen(string word)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(word);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Для вывода в консоль данных (Красный цвет)
        /// </summary>
        /// <param name="word">Текст, который нужно отразить</param>
        private void ConsoleLogRed(string word)
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
            _shopService = new ShopService(cm);
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
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                string nameShop = $"Название магазина{rand.Next(0, 10_000)}";
                Shop? testCreateObject = await _shopService.CreateShop(nameShop);
                if (testCreateObject is null)
                {
                    ConsoleLogRed($"Не удалось создать магазин ''{nameShop}''");
                }
                else
                {
                    ConsoleLogGreen($"О господин, ваш магазин под названием ''{nameShop}'' создан. Это первый шаг к богаству.");
                }
            }
            return true;
        }

        /// <summary>
        /// Удаление магазина
        /// </summary>
        /// <returns></returns>
        public async Task TestDeleteShop()
        {
            for (int i = 5; i < 5; i++) 
            {
                bool testDeleteObject = await _shopService.DeleteShop(i);
                if (!testDeleteObject)
                {
                    ConsoleLogRed($"Не удалось удалить магазин ''{i}''");
                }
                else
                {
                    ConsoleLogGreen($"Ваш магазин удалён ''{i}''. Мы будем скучать по вашему магазину");
                }
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
                if (await _shopService.UpdateNameShop(idListShop[i], newName[i]))
                {
                    ConsoleLogGreen($"О великий, я успешно поменял название на {newName[i]}");
                }
                else 
                {
                   ConsoleLogRed($"Только не бейте, но мне не удалось изменить название вашего магазина на {newName[i]}");
                }
            }
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
            ConsoleLogGreen(message);

            for (int i = 0; i < 10; i++)
            {

                message = await _productService.AddNewCluster($"кластер1{i}");
                // Дочерний элемент
                ConsoleLogGreen(message);
            }
            for (int i = 0; i < 10; i++)
            {

                message = await _productService.AddNewCluster($"кластер_Дочерний{i}", "кластер1");
                // Дочерний элемент
                ConsoleLogGreen(message);
            }
            message = await _productService.AddNewCluster("кластер1", 1);
            // Дочерний элемент
            ConsoleLogGreen(message);

            message = await _productService.AddNewCluster("кластер", 1);
            // Дочерний элемент
            ConsoleLogGreen(message);

            message = await _productService.AddNewCluster("кластер тестирование string", "кластер");
            // Дочерний элемент
            ConsoleLogGreen(message);
        }


        /// <summary>
        /// Обновление кластера
        /// </summary>
        /// <returns></returns>
        public async Task TestUpdateCluster()
        {
            string message = await _productService.UpdateNameCluster(1, "кластер");
            ConsoleLogGreen(message);
            message = await _productService.UpdateNameCluster(1, "test");
            ConsoleLogGreen(message);
            message = await _productService.UpdateNameCluster(10, "test");
            ConsoleLogGreen(message);
            message = await _productService.UpdateNameCluster("test", "UpdateNameCluster");
            ConsoleLogGreen(message);
        }

        /// <summary>
        /// Удаление кластера
        /// </summary>
        /// <returns></returns>
        public async Task TestDeleteCluster()
        {
            string message = await _productService.DeleteCluster(1);
            ConsoleLogGreen(message);
            message = await _productService.DeleteCluster("кластер тестирование string");
            ConsoleLogGreen(message);
            message = await _productService.DeleteCluster(100000);
            ConsoleLogGreen(message);
            message = await _productService.DeleteCluster("Упадёт ли кластер или нет");
            ConsoleLogGreen(message);
        }

        /// <summary>
        /// Обновить позицию кластера в классификаторе
        /// </summary>
        /// <returns></returns>
        public async Task TestUpdatePositionCluster()
        {
            string message = await _productService.UpdatePositionCluster(2, 10);
            ConsoleLogGreen(message);

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
            var message = await _productService.AddProduct(1, 2);
            ConsoleLogGreen(message);
        }
        #endregion

        #region Работа с отзывами (+ рейтинг товара, так как это внутреняя вещь отзывов)

        /// <summary>
        /// Добавить комментарий
        /// </summary>
        /// <returns></returns>
        public async Task TestAddComment()
        {
            string message = await _productService.AddNewComment(1, 1, 1, "Комментарий", 4.3m);
            ConsoleLogGreen(message);
        }

        /// <summary>
        /// Обновить комментарий
        /// </summary>
        /// <returns></returns>
        public async Task TestUpdateComment()
        {
            string message = await _productService.UpdateComment(1, "Комментарий обновлённый", 4.3m);
            ConsoleLogGreen(message);
            message = await _productService.UpdateComment(1, "Комментарий обновлённый ошибка", 4.3m);
            ConsoleLogGreen(message);
        }
        /// <summary>
        /// Удаление(скрыть) комментария пользователя
        /// </summary>
        /// <returns></returns>
        public async Task TestDeleteComment()
        {
            string message = await _productService.DeleteComment(1);
            ConsoleLogGreen(message);
            message = await _productService.DeleteComment(1);
            ConsoleLogGreen(message);
        }


        /// <summary>
        /// Добавить  ответ на комментарий пользователя со стороны магазина
        /// </summary>
        /// <returns></returns>
        public async Task TestAddCommentReply()
        {
            string message = await _productService.AddNewOrUpdateCommentReplyIdCommentUser(1, "Ответный комментарий");
            ConsoleLogGreen(message);

            message = await _productService.AddNewOrUpdateCommentReplyIdCommentReply(1, "Ответный комментарий со стороны Reply");
            ConsoleLogGreen(message);
        }

        /// <summary>
        /// Удалить(скрыть) ответы на комментарии владельцев товара
        /// </summary>
        /// <returns></returns>
        public async Task TestDeleteCommentReply()
        {
            string message = await _productService.AddNewOrUpdateCommentReplyIdCommentUser(1, "Ответный комментарий");
            ConsoleLogGreen(message);

            message = await _productService.AddNewOrUpdateCommentReplyIdCommentReply(1, "Ответный комментарий со стороны Reply");
            ConsoleLogGreen(message);
        }

        /// <summary>
        /// Получить все комментарии по товару
        /// </summary>
        /// <returns></returns>
        public async Task GetAllComment()
        {
            foreach (var item in await _productService.GetCommentProduct(1))
            {
                ConsoleLogGreen($"{item.Text} {item.IsDeleted}");
            }

        }
        #endregion



    }

}
