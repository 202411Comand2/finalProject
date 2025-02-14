using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace BLL.ProductService
{
    /// <summary>
    /// Управление магазином сервис
    /// </summary>
    public class ProductService
    {
        private readonly ShopRepository _shopRepository;
        private readonly UserRepository _userRepository;
        private readonly ClusterRepository _clusterRepository;
        private readonly ProductRepository _productRepository;
        private readonly CommentRepository _commentRepository;
        private readonly CommentReplyRepository _commentReplyRepository;
        private readonly FavoriteRepository _favoriteRepository;
        private readonly RatingRepository _ratingRepository;


        /// <summary>
        /// Конструктор класса 
        /// </summary>
        /// <param name="contextManager"></param>
        public ProductService(IContextManager contextManager)
        {
            _shopRepository = new ShopRepository(contextManager);
            _userRepository = new UserRepository(contextManager);
            _clusterRepository = new ClusterRepository(contextManager);
            _productRepository = new ProductRepository(contextManager);
            _commentRepository = new CommentRepository(contextManager);
            _commentReplyRepository = new CommentReplyRepository(contextManager);
            _favoriteRepository = new FavoriteRepository(contextManager);
            _ratingRepository = new RatingRepository(contextManager);

        }

        #region управление магазином Создание, изменение и удаление
       /*
        /// <summary>
        /// Добавить магазин
        /// </summary>
        /// <param name="name">Имя магазина</param>
        /// <param name="ShopOwner">Владелец магазина</param>
        /// <returns></returns>
        public async Task<string> CreateShop(string name)
        {
            if (await _shopRepository.GetIdByStoreName(name) != -1)
            {
                return "Не удалось создать магазин, так как магазин с таким именем есть в системе";
            }
            Shop newShop = new Shop
            {
                Name = name,
                IsDelete = false,
            };
            var result = await _shopRepository.Add(newShop);
            return $"магазин создан {newShop.Id}";
        }

        /// <summary>
        /// Обновить название магазина
        /// </summary>
        /// <param name="idShop">id магазина</param>
        /// <param name="newNameShop">Новое название магазина</param>
        /// <returns></returns>
        public async Task<string> UpdateShopName(int idShop, string newNameShop)
        {
            Shop? shopUser = await _shopRepository.Get(idShop);
            if (shopUser is null)
            {
                return "В бд такого магазина нет";
            }
            else
            {
                if (await _shopRepository.GetIdByStoreName(newNameShop) == -1)
                {
                    string oldNameShop = shopUser.Name;
                    shopUser.Name = newNameShop;
                    await _shopRepository.Update(shopUser);
                    return $"Название магазина изменено c {oldNameShop} на {newNameShop}";
                }
                else
                {
                    return $"Название ''{newNameShop}'' занято!!!";
                }
            }
        }

        /// <summary>
        /// Обновить название магазина
        /// </summary>
        /// <param name="nameOldShop">Старое название магазина</param>
        /// <param name="newNameShop">Новое название магазина</param>
        /// <returns></returns>
        public async Task<string> UpdateShopName(string nameOldShop, string newNameShop)
        {
            Shop? shopUser = await _shopRepository.GetStoreByName(nameOldShop);
            if (shopUser is null)
            {
                return "В бд за вами такой магазин не закреплён";
            }
            else
            {
                if (await _shopRepository.GetIdByStoreName(newNameShop) == -1)
                {
                    shopUser.Name = newNameShop;
                    await _shopRepository.Update(shopUser);
                    return $"Название магазина изменено c {nameOldShop} на {newNameShop}";
                }
                else
                {
                    return $"Название ''{newNameShop}'' занято!!!";
                }
            }
        }

        /// <summary>
        /// Удаление магазина (скрытие магазина)
        /// </summary>
        /// <param name="nameShop">Название магазина</param>
        /// <param name="idOwner">id владельца</param>
        /// <returns></returns>
        public async Task<string> DeleteShop(string nameShop)
        {
            Shop? shop = await _shopRepository.GetStoreByName(nameShop);
            if (shop is null)
            {
                return "Произошла ошибка при удалении магазина";
            }
            else
            {

                shop.IsDelete = true;
                await _shopRepository.Update(shop);
                return $"магазин ''{nameShop}'' был удалён ";
            }
        }
        */
        #endregion

        #region управление товаром
        /// <summary>
        /// Добавить продукт
        /// </summary>
        /// <param name="idShop">Id магазина</param>
        /// <param name="IdCluster">Id кластер</param>
        /// <returns></returns>
        public async Task<string> AddProduct(int idShop, int IdCluster)
        {
            Shop shop = await _shopRepository.Get(idShop);
            Cluster cluster = await _clusterRepository.Get(IdCluster);
            if (shop is null)
            {
                return "Ошибка магазин по id не найден";
            }
            if (cluster is null)
            {
                return "Ошибка кластер по id не найден";
            }
            Rating rating = new Rating()
            {
                //ProductID = -1,
                AmountOfComments = 1,
                AverageRating = 5,
            };
            
            await _ratingRepository.Add(rating);
            Domain.Entities.Product product = new Domain.Entities.Product()
            {
                Price = 1005.8M,
                Name = "test",
                Barcode = 12345,
                ModelNumber = "123455",
                Description = "Description",
                ClusterId = cluster.Id,
                ShopId = shop.Id,
                RatingId = rating.RatingId
            };

            var s = await _productRepository.Add(product);
            // костыль обсудить!!!
            //product.Cluster = cluster;
            //product.Shop = shop;
            //product.Rating = rating;
            //var s1 = await _productRepository.Update(product);

            //rating.ProductID = product.Id;
            //await _productRepository.Update(product);
            //Product ssss = await _productRepository.Get(product.Id);
            //cluster.Products.Add(product);
            //await _clusterRepository.Update(cluster);

            return "Продукт прикреплён к магазину и кластеру добавлен";
        }
        #endregion

        #region Управление кластером(классификатором)

        /// <summary>
        /// Добавить кластер(классификатор) корневой элемент
        /// </summary>
        /// <param name="name">Имя кластера</param>
        /// <returns></returns>
        public async Task<string> AddNewCluster(string name)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(name);
            if (cluster is null)
            {
                cluster = new Cluster
                {
                    Name = name,
                    ParentId = -1,
                };
                var result = await _clusterRepository.Add(cluster);
                return $"Кластер создан {result.Id}";
            }
            else
            {
                return "Не удалось создать в виду наличия в системе уже существующего кластера";
            }
        }

        /// <summary>
        /// Добавить кластер(классификатор) вложенный
        /// </summary>
        /// <param name="name">Имя кластера</param>
        /// <param name="idParent">Id parent(-1) корень, т.е. располагается на верхнем уровне</param>
        /// <returns></returns>
        public async Task<string> AddNewCluster(string name, int idParent)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(name);
            Cluster clusterParent = await _clusterRepository.Get(idParent);
            if (clusterParent is null)
            {
                return "По указанному id не нашёл родителя";
            }
            if (cluster is null)
            {
                cluster = new Cluster
                {
                    Name = name,
                    ParentId = idParent,
                };
                var result = await _clusterRepository.Add(cluster);
                return "Кластер создан";
            }
            else
            {
                return "Не удалось создать в виду наличия в системе уже существующего кластера";
            }
        }

        /// <summary>
        /// Добавить кластер(классификатор) вложенный
        /// </summary>
        /// <param name="name">Имя кластера</param>
        /// <param name="NameParent">Имя кластера</param>
        /// <returns></returns>
        public async Task<string> AddNewCluster(string name, string NameParent)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(name);
            Cluster clusterParent = await _clusterRepository.GetNameCluster(NameParent);
            if (clusterParent is null)
            {
                return "По указанному имени не нашёл родителя";
            }
            if (cluster is null)
            {
                cluster = new Cluster
                {
                    Name = name,
                    ParentId = clusterParent.Id
                };
                var result = await _clusterRepository.Add(cluster);
                return "Кластер создан";
            }
            else
            {
                return "Не удалось создать в виду наличия в системе уже существующего кластера";
            }
        }

        /// <summary>
        /// Обновить имя кластер
        /// </summary>
        /// <param name="id">id кластера</param>
        /// <param name="newName">Новое имя кластера</param>
        /// <returns></returns>
        public async Task<string> UpdateNameCluster(int id, string newName)
        {
            Cluster cluster = await _clusterRepository.Get(id);
            Cluster clusterNewName = await _clusterRepository.GetNameCluster(newName);

            if (cluster is null)
            {
                return "не получилось изменить название классификатора. Не получилось найти указанный кластер в базе";
            }
            else
            {
                if (clusterNewName is null)
                {
                    cluster.Name = newName;
                    await _clusterRepository.Update(cluster);
                    return "Кластер изменён";
                }
                else
                {
                    return "не получилось изменить название классификатора. Имя этого кластера занято!";
                }
            }
        }

        /// <summary>
        /// Обновить кластер
        /// </summary>
        /// <param name="oldName">Старое имя кластера</param>
        /// <param name="newName">Новое имя кластера</param>
        /// <returns></returns>
        public async Task<string> UpdateNameCluster(string oldName, string newName)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(oldName);
            Cluster clusterNewName = await _clusterRepository.GetNameCluster(newName);

            if (cluster is null)
            {
                return "не получилось изменить название классификатора. Не получилось найти указанный кластер в базе";
            }
            else
            {

                if (clusterNewName is null)
                {
                    cluster.Name = newName;
                    await _clusterRepository.Update(cluster);
                }
                else
                {
                    return "не получилось изменить название классификатора. Имя этого кластера занято!";
                }
            }

            return "Кластер изменён";
        }

        ////ToDO при удалении кластера нужно проверять какие продукты к ним прикреплены?
        /// <summary>
        /// Удаление кластера
        /// </summary>
        /// <param name="idCluster">id кластера</param>
        /// <returns></returns>
        public async Task<string> DeleteCluster(int idCluster)
        {
            Cluster cluster = await _clusterRepository.Get(idCluster);
            if (cluster is null)
            {
                return "Не получилось удалить ввиду отсутствия id кластера";
            }
            else
            {
                await _clusterRepository.Delete(cluster);
                return "Кластер удалён";

            }

        }

        /// <summary>
        /// Удаление кластера
        /// </summary>
        /// <param name="nameCluster">Название кластера</param>
        /// <returns></returns>
        public async Task<string> DeleteCluster(string nameCluster)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(nameCluster);
            if (cluster is null)
            {
                return "Не получилось удалить ввиду отсутствия id кластера";
            }
            else
            {
                await _clusterRepository.Delete(cluster);
                return "Кластер удалён";

            }
        }

        /// <summary>
        ///  Изменение позиции кластера в иерархии
        /// </summary>
        /// <param name="idCluster">id кластера, позицию которого нужно поменять в классификаторе</param>
        /// <param name="idParent">id родителя кластера, куда нужно вложить (-1 означает корень) </param>
        /// <returns></returns>
        public async Task<string> UpdatePositionCluster(int idCluster, int idParent)
        {
            Cluster cluster = await _clusterRepository.Get(idCluster);
            if (cluster is null)
            {
                return "Ошибка. Не найден кластер по id";
            }
            else
            {
                if (idParent == cluster.ParentId)
                {
                    return "Изменения не нужны, так как перемещения не произошло.";
                }
                if (idParent == -1)
                {
                    cluster.ParentId = -1;
                    await _clusterRepository.Update(cluster);
                    return "Изменения были приняты иерархия была изменена";
                }

                Cluster clusterParent = await _clusterRepository.Get(idParent);
                if (clusterParent is not null)
                {
                    cluster.ParentId = idParent;
                    await _clusterRepository.Update(cluster);
                    return "Изменения были приняты иерархия была изменена";
                }
                else
                {
                    return "Ошибка. Родительский кластер не найден!";
                }
            }
        }
        /// <summary>
        ///  Изменение позиции кластера в иерархии
        /// </summary>
        /// <param name="nameCluster">Название кластера</param>
        /// <param name="idParent">id родителя кластера, куда нужно вложить (-1 означает корень) </param>
        /// <returns></returns>
        public async Task<string> UpdatePositionCluster(string nameCluster, int idParent)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(nameCluster);

            if (cluster is null)
            {
                return "Ошибка. Не найден кластер по id";
            }
            else
            {
                if (idParent == cluster.ParentId)
                {
                    return "Изменения не нужны, так как перемещения не произошло.";
                }
                if (idParent == -1)
                {
                    cluster.ParentId = -1;
                    await _clusterRepository.Update(cluster);
                    return "Изменения были приняты иерархия была изменена";
                }

                Cluster clusterParent = await _clusterRepository.Get(idParent);
                if (clusterParent is not null)
                {
                    cluster.ParentId = idParent;
                    await _clusterRepository.Update(cluster);
                    return "Изменения были приняты иерархия была изменена";
                }
                else
                {
                    return "Ошибка. Родительский кластер не найден!";
                }
            }
        }


        /// <summary>
        /// Получить все элементы кластеров
        /// </summary>
        /// <returns>Коллекцию кластеров</returns>
        public async Task<List<Cluster>> GetAllElementsCluster() => (List<Cluster>)await _clusterRepository.GetAll();

        /// <summary>
        /// Получить только корневые элементы кластера
        /// </summary>
        public async Task<List<Cluster>> GetRootElementsCluster() => await _clusterRepository.GetRootElementsClaster();


        ////TODO что делать ошибку кидать или возвращаться null
        /// <summary>
        /// Получить дочерние элементы кластера
        /// </summary>
        /// <param name="idCluster">Id кластера</param>
        /// <returns></returns>
        public async Task<List<Cluster>> GetChildrenElementsCluster(int idCluster)
        {
            Cluster cluster = await _clusterRepository.Get(idCluster);
            if (cluster is null)
            {
                //если такого кластера нет
                return null;
            }
            else
            {
                return await _clusterRepository.GeElementsClaster(idCluster);
            }
        }

        /// <summary>
        /// Получить дочерние элементы кластера
        /// </summary>
        /// <param name="nameCluster">Имя кластера</param>
        /// <returns></returns>
        public async Task<List<Cluster>> GetChildrenElementsCluster(string nameCluster)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(nameCluster);

            if (cluster is null)
            {//"Ошибка. Не найден кластер по имени"
                return null;
            }
            return await _clusterRepository.GeElementsClaster(cluster.Id);

        }



        #endregion

        #region управление отзывами

        #region отзывы покупателей

        /// <summary>
        /// Получить все комментарии по продукту
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        public async Task<List<Comment>> GetCommentProduct(int idProduct)
        {
            Domain.Entities.Product? product = await _productRepository.Get(idProduct);
            if (product is not null)
            {
                return await _commentRepository.GetAllCommentOnTheProduct(idProduct);
            }
            return null;
        }

        ////TODO  как проверить, что пользователь купил товар и что он на него может оставить отзыв?
        /// <summary>
        /// Добавить отзыв на товар
        /// </summary>
        /// <param name="idUser">id пользователя</param>
        /// <param name="idShop">id магазина</param>
        /// <param name="idProduct">id продукта</param>
        /// <param name="textComment">Текст комментария</param>
        /// <returns></returns>
        public async Task<string> AddNewComment(int idUser, int idShop, int idProduct, string textComment, decimal estimation)
        {
            Comment? comment = await _commentRepository.GetCommentUser(idUser, idProduct);
            if (await _shopRepository.Get(idShop) is null)
            {
                return "Ошибка. Отсутствует магазин!";
            }
            if (await _productRepository.Get(idProduct) is null)
            {
                return "Ошибка. Отсутствует продукт!";
            }
            if (await _userRepository.Get(idUser) is null)
            {
                return "Ошибка. Отсутствует пользователь!";
            }
                Domain.Entities.Product product = await _productRepository.Get(idProduct);
                if (comment is null)
            {
                CommentReply reply = new CommentReply()
                {
                    Text = "",
                };
                reply = await _commentReplyRepository.Add(reply);
                comment = new Comment
                {
                    Estimation = estimation,
                    Text = textComment,
                    UserName = "null",
                    UserId = idUser,
                    ShopId = idShop,
                    IdProduct = idProduct,
                    IdReply = reply.Id
                };
                var result = await _commentRepository.Add(comment);
                await UpdateRatingProduct(comment, "create", 0);
                return $"Комментарий создан {comment.Id}";
            }
            else
            {
                return "Не удалось создать новый комментарий комментарий ввиду наличия";
            }
        }

        /// <summary>
        /// Обновление комментария
        /// </summary>
        /// <param name="idComment">id комментария</param>
        /// <param name="textComment">Новый текст комментария</param>
        /// <param name="estimation">Новая оценка комментария</param>
        /// <returns></returns>
        public async Task<string> UpdateComment(int idComment, string textComment, decimal estimation)
        {
            Comment? comment = await _commentRepository.Get(idComment);
            if (comment is not null)
            {
                decimal oldEstimation = comment.Estimation;
                comment.Text = textComment;
                comment.Estimation = estimation;

                var result = await _commentRepository.Update(comment);
                await UpdateRatingProduct(comment, "update", oldEstimation);
                return $"Комментарий обновлён {comment.Id}";
            }
            else
            {
                return "Не удалось обновить комментарий из-за отсутствия его в бд";
            }
        }

        /// <summary>
        /// Удаление комментарий (скрыть IsDeleted = true)
        /// </summary>
        /// <param name="idComment">id комментария</param>
        /// <returns></returns>
        public async Task<string> DeleteComment(int idComment)
        {
            Comment? comment = await _commentRepository.Get(idComment);
            if (comment is not null)
            {

                CommentReply commentReply = await _commentReplyRepository.Get(comment.IdReply);
                comment.IsDeleted = true;
                commentReply.IsDeleted = true;
                await _commentRepository.Update(comment);
                await _commentReplyRepository.Update(commentReply);
                await UpdateRatingProduct(comment, "delete", 0);
                return "Комментарий удалён";
            }
            else
            {
                return "Не удалось удалить комментарий из-за отсутствия его в бд";
            }
        }
        #endregion

        #region отзывы продовцов по 2 перегрузки

        /// <summary>
        /// Добавить(Обновить) ответ на комментарий пользователя со стороны магазина (id комментария пользователя)
        /// </summary>
        /// <param name="idCommentUser">id комментария пользователя</param>
        /// <param name="textComment">Текст комментария</param>
        /// <returns></returns>
        public async Task<string> AddNewOrUpdateCommentReplyIdCommentUser(int idCommentUser, string textComment)
        {
            Comment? comment = await _commentRepository.Get(idCommentUser);
            if (comment is not null)
            {
                CommentReply commentReply = await _commentReplyRepository.Get(comment.IdReply);
                commentReply.Text = textComment;
                try
                {
                    await _commentReplyRepository.Update(commentReply);
                    // если пользователь удалит комментарий, то ответный тоже будет удалён "каскадно",то в теории может упасть сервер
                    return $"Создан комментарий ответ {commentReply.Id}";
                }
                catch
                {
                    return $"Создан комментарий ответ {commentReply.Id}";
                }

            }
            else
            {
                return "Не удалось создать новый комментарий комментарий ввиду отсутствия пользовательского комментария";
            }
        }


        /// <summary>
        /// Добавить(Обновить) ответ на комментарий пользователя со стороны магазина (id комментария ответного)
        /// </summary>
        /// <param name="IdCommentReply">id комментария ответа</param>
        /// <param name="textComment">Текст комментария</param>
        /// <returns></returns>
        public async Task<string> AddNewOrUpdateCommentReplyIdCommentReply(int IdCommentReply, string textComment)
        {
            CommentReply commentReply = await _commentReplyRepository.Get(IdCommentReply);
            if (commentReply is not null)
            {
                commentReply.Text = textComment;
                await _commentReplyRepository.Update(commentReply);
                try
                {
                    await _commentReplyRepository.Update(commentReply);
                    // если пользователь удалит комментарий, то ответный тоже будет удалён "каскадно",то в теории может упасть сервер
                    return $"Создан комментарий ответ {commentReply.Id}";
                }
                catch
                {
                    return $"Создан комментарий ответ {commentReply.Id}";
                }
            }
            else
            {
                return "Не удалось создать новый комментарий комментарий ввиду отсутствия пользовательского комментария";
            }
        }


        /// <summary>
        /// Удалить (скрыть IsDeleted = true) ответ на комментарий пользователя со стороны магазина (id комментария пользователя)
        /// </summary>
        /// <param name="idCommentUser">id комментария пользователя</param>
        /// <returns></returns>
        public async Task<string> DeleteCommentReplyIdCommentUser(int idCommentUser)
        {
            Comment? comment = await _commentRepository.Get(idCommentUser);
            if (comment is not null)
            {
                CommentReply commentReply = await _commentReplyRepository.Get(comment.IdReply);

                commentReply.IsDeleted = true;
                await _commentReplyRepository.Update(commentReply);
                return $"Комментарий удалён {commentReply.Id}";

            }
            else
            {
                return "Не удалось создать новый комментарий комментарий ввиду отсутствия пользовательского комментария";
            }
        }


        /// <summary>
        /// Удалить (скрыть IsDeleted = true) ответ на комментарий пользователя со стороны магазина (id комментария ответного)
        /// </summary>
        /// <param name="IdCommentReply">id комментария ответа</param>
        /// <returns></returns>
        public async Task<string> DeleteCommentReplyIdCommentReply(int IdCommentReply)
        {
            CommentReply? commentReply = await _commentReplyRepository.Get(IdCommentReply);
            if (commentReply is not null)
            {

                commentReply.IsDeleted = true;
                await _commentReplyRepository.Update(commentReply);
                return $"Комментарий удалён {commentReply.Id}";


            }
            else
            {
                return "Не удалось удалить комментарий ввиду отсутствия";
            }
        }


        #endregion

        #endregion



        #region Избранные позиции пользователя
        /// <summary>
        /// Добавить товар в избранное
        /// </summary>
        /// <param name="idUser">Id пользователя</param>
        /// <param name="idProduct">Id продукта</param>
        /// <returns></returns>
        public async Task<string> AddFavoriteProduct(int idUser, int idProduct)
        {
            User shop = await _userRepository.Get(idUser);

            Domain.Entities.Product product = await _productRepository.Get(idProduct);
            if (product is null)
            {
                return "Ошибка. Товар не найден!";
            }
            if (shop is null)
            {
                return "Ошибка. Пользователь не найден!";
            }
            if (await _favoriteRepository.GetFavoriteUser(idUser, idProduct) is null)
            {
                return "Ошибка. Указанная позиция в избранном уже состоит";
            }
            var _favorite = new Favorite()
            {
                UserId = idUser,
                IdProduct = idProduct,

            };
            await _favoriteRepository.Add(_favorite);
            return "Продукт прикреплён к магазину и кластеру добавлен";

        }
        /// <summary>
        /// Удалить товар из избранного
        /// </summary>
        /// <param name="idFavorite">Id избранной позиции</param>
        /// <returns></returns>
        public async Task<string> DeleteFavoriteProduct(int idFavorite)
        {
            Favorite favorite = await _favoriteRepository.Get(idFavorite);
            await _favoriteRepository.Delete(favorite);
            return "Удалалил из избранного";
        }

        #endregion


        #region работа с рейтингом товара

        /// <summary>
        /// Работа с рейтингом товара
        /// </summary>
        /// <param name="comment">объект комментарий</param>
        /// <param name="ratingCommand">Команда рейтинга</param>
        /// <param name="oldEstimation">Старая оценка</param>
        /// <returns></returns>
        private async Task<string> UpdateRatingProduct(Comment comment, string ratingCommand,  decimal oldEstimation)
        {
            //если новая оценка, то пересчёт рейтинга делать не нужно
            var comment1 = await _commentRepository.Get(comment.Id);
            var product = await _productRepository.Get(comment.IdProduct);
            Rating rating = await _ratingRepository.Get(product.RatingId);

            switch (ratingCommand)
            {
                case "create":
                    rating = new Rating()
                    {
                        AmountOfComments = 1,
                        AverageRating = comment.Estimation,
                        ProductID = product.Id
                    };
                    await _ratingRepository.Add(rating);
                    return "Создан новый рейтинг для продукта";
                case "update":
                    decimal newRatingUser = oldEstimation - comment.Estimation;
                    rating.AverageRating = (rating.AverageRating * rating.AmountOfComments + newRatingUser) / rating.AmountOfComments;
                    await _ratingRepository.Update(rating);
                    return "Комментарий удалён, рейтинг обновлён";
                case "delete":
                    int countComment = rating.AmountOfComments - 1 <= 1 ? 1 : rating.AmountOfComments - 1;
                    // rating = (await _productRepository.Get(comment.IdProduct)).Rating;
                    rating.AverageRating = (rating.AverageRating * rating.AmountOfComments - comment.Estimation) / countComment;
                    rating.AmountOfComments--;
                    await _ratingRepository.Update(rating);
                    return "Комментарий удалён, рейтинг обновлён";

            }


            return "";
        }

        #endregion


        private decimal rating() 
        {
            return 0;
        }
    }
}
