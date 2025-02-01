using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns>String результата</returns>
        public async Task<string> CreateOwnerShop(bool isHost, User user = null) 
        {
            if (user == null)
            {
                return "Произошла ошибка при создании владельца магазина. " +
                    "Такого пользователя нет в Базе данных.";
            }
            else 
            {
               

                var newShopOwner = new ShopOwner
                {
                    IsHost = isHost,
                    IsDelete = false, 
                    // при создании владельца он никак не может быть удалён
                    UserId = user.Id,
                    ShopId = 0,
                };

                var result2 = await _shopOwnerRepository.Add(newShopOwner);
                return "Владелец магазина зарегистрирована";
            }
        }
        /// <summary>
        /// Добавить магазин
        /// </summary>
        /// <param name="name">Имя магазина</param>
        /// <param name="ShopOwner">Владелец магазина</param>
        /// <returns></returns>
        public async Task<string> CreateShop(string name, ShopOwner ShopOwner) 
        {
            if (await _shopRepository.CheckNameShop(name)) 
            {
                return $"Ошибка. Имя ''{name}'', которые вы придумали для магазина занято";
            }

            Domain.Entities.Shop newShop = new Domain.Entities.Shop
            {
                Name = name,
                ShopOwerId = ShopOwner.Id,
                IsDelete = false,
            };
            var result = await _shopRepository.Add(newShop);
            return "Магазин создан"; 
        }
        //public async Task<User> CreateShopOwer() 
        //{

        //}
    }
}
