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
using BLL.Products;


namespace Test
{
    internal class BLLShopServiceTest : IBLLShopServiceTest
    {
        private UserRepository _userRepository;
        private ProductService _productService;
        private ShopService _shopService; // Для создания магазина 
        private ClusterService _clusterService;
        private CommentService _commentService;
        private CommentReplyService _commentReplyService;
        private FavoriteProductService _favoriteProductService;

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
            Console.ForegroundColor = ConsoleColor.Red;
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
            _clusterService = new ClusterService(cm);
            _commentService = new CommentService(cm);
            _commentReplyService = new CommentReplyService(cm);
            _favoriteProductService = new FavoriteProductService(cm);
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
            Random rand = new();
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
            // корневой элемент
            if (await _clusterService.AddNewCluster("кластер"))
            {
                ConsoleLogGreen("Кластер создан");
            }
            else
            {
                ConsoleLogRed("Не удалось создать кластер");
            }
            Console.WriteLine("Попытка создать 10 кластеров (корневые элементы)");
            for (int i = 0; i < 10; i++)
            {
                if (await _clusterService.AddNewCluster($"кластер1{i}"))
                {
                    ConsoleLogGreen($"Кластер создан,  название: кластер1{i}");
                }
                else
                {
                    ConsoleLogRed("Не удалось создать кластер, название: кластер1{i}");
                }
            }
            Console.WriteLine("Попытка создать 10 кластеров (вложенных)");

            for (int i = 0; i < 10; i++)
            {
                if (await _clusterService.AddNewCluster($"кластер_Дочерний{i}", "кластер1"))
                {
                    ConsoleLogGreen("Кластер создан");
                }
                else
                {
                    ConsoleLogRed("Не удалось создать кластер");
                }
            }
            Console.WriteLine("Испытания перегрузок кластера:");
            if (await _clusterService.AddNewCluster("кластер1", 1))
            {
                ConsoleLogGreen("Кластер создан");
            }
            else
            {
                ConsoleLogRed("Не удалось создать кластер");
            }
            if (await _clusterService.AddNewCluster("кластер", 1))
            {
                ConsoleLogGreen("Кластер создан");
            }
            else
            {
                ConsoleLogRed("Не удалось создать кластер");
            }
            if (await _clusterService.AddNewCluster("кластер тестирование string", "кластер"))
            {
                ConsoleLogGreen("Кластер создан");
            }
            else
            {
                ConsoleLogRed("Не удалось создать кластер");
            }
        }


        /// <summary>
        /// Обновление кластера
        /// </summary>
        /// <returns></returns>
        public async Task TestUpdateCluster()
        {
            Console.WriteLine("Попытка обновить кластер");
          
            if (await _clusterService.UpdateNameCluster(1, "кластер"))
            {
                ConsoleLogGreen("(id и новое имя) наименование кластера обновлено");
            }
            else
            {
                ConsoleLogRed("(id и новое имя) наименование кластера не обновлено");
            }
            if (await _clusterService.UpdateNameCluster(10, "кластер"))
            {
                ConsoleLogGreen("(id и новое имя) наименование кластера обновлено");
            }
            else
            {
                ConsoleLogRed("(id и новое имя) наименование кластера не обновлено");
            }
  
            if (await _clusterService.UpdateNameCluster("test", "кластер"))
            {
                ConsoleLogGreen("(старое имя и новое имя) наименование кластера обновлено");
            }
            else
            {
                ConsoleLogRed("(старое имя и новое имя) наименование кластера не обновлено");
            }
        }

        /// <summary>
        /// Удаление кластера
        /// </summary>
        /// <returns></returns>
        public async Task TestDeleteCluster()
        {
            if (await _clusterService.DeleteCluster(1))
            {
                ConsoleLogGreen("(id) Кластер удалён id = 1");
            }
            else
            {
                ConsoleLogRed("(id) Кластер не удалён id = 1");
            }
            if (await _clusterService.DeleteCluster("кластер тестирование string"))
            {
                ConsoleLogGreen("(Наименование кластера) Кластер удалён Наименование = кластер тестирование string");
            }
            else
            {
                ConsoleLogRed("(Наименование кластера) Кластер не удалён Наименование = кластер тестирование string");
            }

            if (await _clusterService.DeleteCluster(100000))
            {
                ConsoleLogGreen("(id) Кластер удалён id = 100000");
            }
            else
            {
                ConsoleLogRed("(id) Кластер не удалён id = 100000");
            }

            if (await _clusterService.DeleteCluster("Упадёт ли кластер или нет"))
            {
                ConsoleLogGreen("(Наименование кластера) Кластер удалён Наименование = Упадёт ли кластер или нет");
            }
            else
            {
                ConsoleLogRed("(Наименование кластера) Кластер не удалён Наименование = Упадёт ли кластер или нет");
            }
        }

        /// <summary>
        /// Обновить позицию кластера в классификаторе
        /// </summary>
        /// <returns></returns>
        public async Task TestUpdatePositionCluster()
        {
            if (await _clusterService.UpdatePositionCluster(2, 10))
            {
                ConsoleLogGreen("(id кластера, id родителя) Позиция кластера изменена ");
            }
            else
            {
                ConsoleLogGreen("(id кластера, id родителя) Позиция кластера не изменена");
            }

        }

        /// <summary>
        /// Получить кластеры по определённым условиям
        /// </summary>
        /// <returns></returns>
        public async Task TestGetCluster()
        {
            Console.WriteLine("\nПолучить все элементы кластеров:");
            foreach (var item in await _clusterService.GetAllElementsCluster())
            {
                Console.WriteLine($"{item.Name} {item.ParentId}");

            }

            Console.WriteLine("\nПолучить только корневые элементы кластера:");
            foreach (var item in await _clusterService.GetRootElementsCluster())
            {
                Console.WriteLine($"{item.Name} {item.ParentId}");
            }
            Console.WriteLine("\nПолучить дочерние элементы кластера:");
            foreach (var item in await _clusterService.GetChildrenElementsCluster(2))
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
            if (await _commentService.AddNewComment(1, 1, 1, "Комментарий", 4.3m))
            {
                ConsoleLogGreen($"О великий, коментарий создан ");
            }
            else
            {
                ConsoleLogRed($"Только не бейте, но коментарий не был создан");
            }
        }

        /// <summary>
        /// Обновить комментарий
        /// </summary>
        /// <returns></returns>
        public async Task TestUpdateComment()
        {
            if (await _commentService.UpdateComment(1, "Комментарий обновлённый", 4.3m))
            {
                ConsoleLogGreen($"коментарий Обновлён");
            }
            else
            {
                ConsoleLogRed($"Коментарий Не Обновлён");
            }
            if (await _commentService.UpdateComment(1, "Комментарий обновлённый ошибка", 4.3m))
            {
                ConsoleLogGreen($"коментарий Обновлён");
            }
            else
            {
                ConsoleLogRed($"Коментарий Не Обновлён");
            }
        }
        /// <summary>
        /// Удаление(скрыть) комментария пользователя
        /// </summary>
        /// <returns></returns>
        public async Task TestDeleteComment()
        {
            if( await _commentService.DeleteComment(1))
            {
                ConsoleLogGreen($"коментарий удалён");
            }
            else
            {
                ConsoleLogRed($"Коментарий Не удалён");
            }

            if (await _commentService.DeleteComment(1))
            {
                ConsoleLogGreen($"коментарий удалён");
            }
            else
            {
                ConsoleLogRed($"Коментарий Не удалён");
            }
        }


        /// <summary>
        /// Добавить  ответ на комментарий пользователя со стороны магазина
        /// </summary>
        /// <returns></returns>
        public async Task TestAddCommentReply()
        {
            if (await _commentReplyService.AddReplyComment(1, "Ответный комментарий")) 
            {
                ConsoleLogGreen($"Ответный комментарий добавлен");
            }
            else
            {
                ConsoleLogRed($"Не получилось добавить ответный комментарий");
            }


            if (await _commentReplyService.AddReplyComment(1, "Ответный комментарий со стороны Reply"))
            {
                ConsoleLogGreen($"Ответный комментарий добавлен");
            }
            else
            {
                ConsoleLogRed($"Не получилось добавить ответный комментарий");
            }
           
        }

        /// <summary>
        /// Удалить(скрыть) ответы на комментарии владельцев товара
        /// </summary>
        /// <returns></returns>
        public async Task TestDeleteCommentReply()
        {
            if (await _commentReplyService.DeleteCommentReply(2))
            {
                ConsoleLogGreen($"Удалён отвеный комментарий");
            }
            else
            {
                ConsoleLogRed($"Добавлен ответный комментарий");
            }
          

            //message = await _productService.AddNewOrUpdateCommentReplyIdCommentReply(1, "Ответный комментарий со стороны Reply");
            //ConsoleLogGreen(message);
        }

        /// <summary>
        /// Получить все комментарии по товару
        /// </summary>
        /// <returns></returns>
        public async Task GetAllComment()
        {
            foreach (var item in await _commentService.GetCommentProduct(1))
            {
                ConsoleLogGreen($"{item.Text} {item.IsDeleted}");
            }

        }
        #endregion


        #region тестирование избранных позиция магазина
        /// <summary>
        /// Добовление позиции в избраное
        /// </summary>
        /// <returns></returns>
        public async Task TestAddFavoriteProduct()
        {
            if (await _favoriteProductService.AddFavoriteProduct(1, 1))
            {
                ConsoleLogGreen($"Позиция добавлнена в избранное");
            }
            else
            {
                ConsoleLogRed($"Позиция не добавлнена в избранное");
            }
            //if (await _favoriteProductService.AddFavoriteProduct(-1212, 12))
            //{
            //    ConsoleLogGreen($"Позиция добавлнена в избранное");
            //}
            //else
            //{
            //    ConsoleLogRed($"Позиция не добавлнена в избранное");
            //}
        }
        /// <summary>
        /// Удаление избраной позиции
        /// </summary>
        /// <returns></returns>
        public async Task TestDeleteFavoriteProduct()
        {
            if (await _favoriteProductService.DeleteFavoriteProduct(1))
            {
                ConsoleLogGreen($"Позиция удалена избранного");
            }
            else
            {
                ConsoleLogRed($"Позиция не удалена из избранного");
            }
            if (await _favoriteProductService.DeleteFavoriteProduct(1111))
            {
                ConsoleLogGreen($"Позиция удалена избранного");
            }
            else
            {
                ConsoleLogRed($"Позиция не удалена из избранного");
            }
        }
        #endregion
    }

}
