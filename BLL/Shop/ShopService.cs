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
    public class ShopService
    {
        private readonly ShopRepository _shopRepository;
        private readonly ShopOwnerRepository _shopOwnerRepository;
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Конструктор класса 
        /// </summary>
        /// <param name="contextManager"></param>
        public ShopService(IContextManager contextManager)
        {
            _shopRepository = new ShopRepository(contextManager);
            _shopOwnerRepository = new ShopOwnerRepository(contextManager);
            _userRepository = new UserRepository(contextManager);
        }


        /// <summary>
        /// Создать владельца магазина 
        /// </summary>
        /// <param name="isHost">Владелец магазина</param>
        /// <param name="user">Пользователь, который создаёт магазин</param>
        /// <param name="nameShop">Название магазина</param>
        /// <returns>String результата</returns>
        public async Task<string> CreateOwnerShop(bool isHost, User user, string nameShop) 
        {
            if (user == null)
            {
                return "Произошла ошибка при создании владельца магазина. " +
                    "Такого пользователя нет в Базе данных.";
            }
            else 
            {
                if (await _shopRepository.CheckNameShop(nameShop))
                {
                    return $"Ошибка. Имя ''{nameShop}'', которые вы придумали для магазина занято";
                }

                ShopOwner newShopOwner = new ShopOwner
                {
                    IsHost = isHost,
                    IsDelete = false, 
                    // при создании владельца он никак не может быть удалён
                    UserId = user.Id,
                    ShopId = 0,
                };
                await _shopOwnerRepository.Add(newShopOwner);
                // занесения владельца магазина в бд
                newShopOwner.ShopId = await  CreateShop(nameShop, newShopOwner);
                // создание магазина
                await _shopOwnerRepository.Update(newShopOwner);
                //обновление владельца магазина(добавление ему id на его новый магазин)
                return "Владелец магазина зарегистрирована";
            }
        }


        /// <summary>
        /// Добавить магазин
        /// </summary>
        /// <param name="name">Имя магазина</param>
        /// <param name="ShopOwner">Владелец магазина</param>
        /// <returns></returns>
        private async Task<int> CreateShop(string name, ShopOwner ShopOwner) 
        {
            Domain.Entities.Shop newShop = new Domain.Entities.Shop
            {
                Name = name,
                ShopOwerId = ShopOwner.Id,
                IsDelete = false,
            };
            var result = await _shopRepository.Add(newShop);
            return result.Id; 
        }


        /// <summary>
        /// Получить магазины пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns></returns>
        public async Task<List<Domain.Entities.Shop>> GetShopUser(int id) 
        {
            var list = await _shopRepository.GetShopUser(id);

            if (list.Count == 0)
            {
                return null;
            }
            else 
            {
                return list;
            }
        }

        /// <summary>
        /// Удаление магазина (скрытие магазина)
        /// </summary>
        /// <param name="nameShop">Название магазина</param>
        /// <param name="idOwner">id владельца</param>
        /// <returns></returns>
        public async Task<string> DeleteShop(string nameShop, int idOwner) 
        {
            var s = await _shopRepository.CheckNameShop(nameShop);
            throw new NotImplementedException("не реализовано");
            return "Магазин удалён";
        }


        /// <summary>
        /// Обновить название магазина
        /// </summary>
        /// <param name="idUser">id пользователя</param>
        /// <param name="idShop">id магазина</param>
        /// <param name="newNameShop">Новое название магазина</param>
        /// <returns></returns>
        public async Task<string> UpdateShopName(int idUser, int idShop, string newNameShop)
        {
            Domain.Entities.Shop? shopUser =  await _shopRepository.CheckOwnerShopUser(idUser, idShop);
            if (shopUser is null)
            {
                return "В бд за вами такой магазин не закреплён";
            }
            else
            {
                string oldNameShop = shopUser.Name;
                shopUser.Name = newNameShop;
                await _shopRepository.Update(shopUser);
                return $"Название магазина изменено c {oldNameShop} на {newNameShop}";
            }
        }
        /// <summary>
        /// Обновить название магазина
        /// </summary>
        /// <param name="idUser">id пользователя</param>
        /// <param name="nameOldShop">Старое название магазина</param>
        /// <param name="newNameShop">Новое название магазина</param>
        /// <returns></returns>
        public async Task<string> UpdateShopName(int idUser, string nameOldShop, string newNameShop)
        {
            Domain.Entities.Shop? shopUser = await _shopRepository.CheckOwnerShopUser(idUser, nameOldShop);
            if (shopUser is null)
            {
                return "В бд за вами такой магазин не закреплён";
            }
            else
            {
                string oldNameShop = shopUser.Name;
                shopUser.Name = newNameShop;
                await _shopRepository.Update(shopUser);
                return $"Название магазина изменено c {oldNameShop} на {newNameShop}";
            }
        }
    }
}
