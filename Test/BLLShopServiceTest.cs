using BLL.Identity;
using BLL.Shop;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstractions;
using Domain.Entities;


namespace Test
{
    internal class BLLShopServiceTest
    {
        private UserRepository _userRepository;
        private ShopService _shopService;
        private ShopRepository _shopRepository;
        private ShopOwnerRepository _shopOwnerRepository;


        /// <summary>
        /// Тестирование создание магазина пользователем
        /// </summary>
        /// <returns></returns>

        public BLLShopServiceTest(IContextManager cm) 
        {
            _shopService = new ShopService(cm);
            _userRepository = new UserRepository(cm);
            _shopRepository = new ShopRepository(cm);
            _shopOwnerRepository = new ShopOwnerRepository(cm);
        }
        /// <summary>
        /// Создание Владельца магазина
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CreateShopOwner() 
        {
            User user = await _userRepository.Get(1);
            if (user == null)
            {
                return false;
            }
            var message = await _shopService.CreateOwnerShop(true,user);

            // для теста получаем пользователя предполагаю, что пользователь будет приходить из вне
            //await addShop.CreateShopUser();
            return true;
        }
        /// <summary>
        /// Создание магазина
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CreateShop() 
        {
            ShopOwner shopOwner = await _shopOwnerRepository.Get(2);
            if (shopOwner == null)
            {
                return false;
            }
            var message = await _shopService.CreateShop("Название магазина", shopOwner);
            return true;
        }
    }
}
