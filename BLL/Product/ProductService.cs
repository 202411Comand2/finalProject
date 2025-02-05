using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Shop
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
            _ratingRepository = new RatingRepository(contextManager);

        }

        #region управление магазином Создание, изменение и удаление
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
             Domain.Entities.Shop newShop = new Domain.Entities.Shop
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
            Domain.Entities.Shop? shopUser = await _shopRepository.Get(idShop);
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
            Domain.Entities.Shop? shopUser = await _shopRepository.GetStoreByName(nameOldShop);
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
            Domain.Entities.Shop? shop = await _shopRepository.GetStoreByName(nameShop);
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
            Domain.Entities.Shop  shop = await _shopRepository.Get(idShop);
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
                ProductID = -1,
                AmountOfComments = 0,
                AverageRating = 0,
            };

            var s =   await _ratingRepository.Add(rating);


            Product product = new Product()
            {
                Price = 1005.8M,
                Name = "test",
                Barcode = 12345,
                ModelNumber ="123455",
                Description = "Description",
                ClusterId = cluster.Id,
               // Cluster = cluster, // надо указывать только ссылку
              //  Shop = shop,
                ShopId = shop.Id,
               // Rating = s,
                RatingId=rating.RatingId

            };
            await _productRepository.Add(product);
            return "Продукт прикрёплен к магазину и кластеру добавлен";
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
                return $"Кластер создан {cluster.Id}";
            }
            else
            {
                return "Не удалось создать в виду налиичя в системе уже сощетвующего класстера";
            }
        }

        /// <summary>
        /// Добавить кластер(классификатор) вложенный
        /// </summary>
        /// <param name="name">Имя кластера</param>
        /// <param name="idPerent">Id perent(-1) корень, т.е. располагается на врехнем уровне</param>
        /// <returns></returns>
        public async Task<string> AddNewCluster(string name, int idPerent)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(name);
            Cluster clusterPerent = await _clusterRepository.Get(idPerent);
            if (clusterPerent is null)
            {
                return "По указаному id не нашёл родителя";
            }
            if (cluster is null)
            {
                cluster = new Cluster
                {
                    Name = name,
                    ParentId = idPerent,
                };
                var result = await _clusterRepository.Add(cluster);
                return "Кластер создан";
            }
            else
            {
                return "Не удалось создать в виду налиичя в системе уже сощетвующего класстера";
            }
        }

        /// <summary>
        /// Добавить кластер(классификатор) вложенный
        /// </summary>
        /// <param name="name">Имя кластера</param>
        /// <param name="idPerent">Id perent(-1) корень, т.е. располагается на врехнем уровне</param>
        /// <returns></returns>
        public async Task<string> AddNewCluster(string name, string NamePerent)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(name);
            Cluster clusterPerent = await _clusterRepository.GetNameCluster(NamePerent);
            if (clusterPerent is null)
            {
                return "По указаному именни не нашёл родителя";
            }
            if (cluster is null)
            {
                cluster = new Cluster
                {
                    Name = name,
                    ParentId = clusterPerent.Id
                };
                var result = await _clusterRepository.Add(cluster);
                return "Кластер создан";
            }
            else
            {
                return "Не удалось создать в виду налиичя в системе уже сощетвующего класстера";
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

        ////ToDO при удалении кластера нужно проверять какие продукты к ним прикрепдены?
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
                return "Не получилось удалить ввиду отсутствия id класетра";
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
        /// <param name="nameClaster">Название кластера</param>
        /// <returns></returns>
        public async Task<string> DeleteCluster(string nameClaster)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(nameClaster);
            if (cluster is null)
            {
                return "Не получилось удалить ввиду отсутствия id класетра";
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

                Cluster clusterPerent = await _clusterRepository.Get(idParent);
                if (clusterPerent is not null)
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

                Cluster clusterPerent = await _clusterRepository.Get(idParent);
                if (clusterPerent is not null)
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
                return  await _clusterRepository.GeElementsClaster(idCluster);
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

        public async Task<string> AddComment(int idProduct, int shopId, int idUser, decimal productEvaluation) 
        {
            Comment comment = new Comment()
            {
                Text ="",
                Estimation= productEvaluation,
                UserId= idUser,
                ShopId = shopId
            };
        }

        #endregion
    }
}
