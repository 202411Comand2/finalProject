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
            _shopService = new ShopService(cm);
            _userRepository = new UserRepository(cm);
            _shopRepository = new ShopRepository(cm);
            _shopOwnerRepository = new ShopOwnerRepository(cm);
        }
        /// <summary>
        /// Создание Владельца магазина
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CreateShopOwnerAndShop()
        {
            User user = await _userRepository.Get(1);
            if (user == null)
            {
                return false;
            }
            Random rand = new Random();
            string message = await _shopService.CreateOwnerShop(true, user, $"Название магазина{rand.Next(0, 10_000)}");
            ConsoleLog(message);
            // для теста получаем пользователя предполагаю, что пользователь будет приходить из вне
            //await addShop.CreateShopUser();
            return true;
        }

        ///переделанная логика
        /// <summary>
        /// Создание магазина
        /// </summary>
        /// <returns></returns>
        //public async Task<bool> CreateShop() 
        //{
        //    ShopOwner shopOwner = await _shopOwnerRepository.Get(2);
        //    if (shopOwner == null)
        //    {
        //        return false;
        //    }
        //    string message = await _shopService.CreateShop("Название магазина", shopOwner);
        //    ConsoleLog(message);
        //    return true;
        //}

        /// <summary>
        /// Проверка на удаление магазина 
        /// </summary>
        /// <returns></returns>
        public async Task testSearchNameShop()
        {
            List<string> l = new List<string>() { "we", "12", "Название магазина" };
            foreach (string s in l)
            {
                string message = await _shopService.DeleteShop(s, 0);
                ConsoleLog(message);

            }
        }

        /// <summary>
        /// Вывести список магазинов пользователя
        /// </summary>
        /// <returns></returns>
        public async Task TestGetShopUser()
        {
            int id = 1;
            ConsoleLog($"Список магазинов пользователя под id: {id}");
            foreach (var shopUser in await _shopService.GetShopUser(1))
            {
                ConsoleLog($"{shopUser.Id} {shopUser.Name}");
            }
        }

        /// <summary>
        /// Изменить имя магазина
        /// </summary>
        /// <returns></returns>
        public async Task TestUpdateNameShop() 
        {
            List<int>idListUser = new List<int>() { 1,1,1};
            List<int> idListShop = new List<int>() { 5, 6, 3 };
            List<string> newName = new List<string>() { "we", "12", "test" };
            List<string> oldNameShop = new List<string>() { "we", "12", "test" };

            for (int i = 0; i < idListUser.Count; i++) 
            {
                string message = await _shopService.UpdateShopName(idListUser[i], idListShop[i], newName[i]);
                ConsoleLog(message+$"\n {idListUser[i]} {idListShop[i]} { newName[i]}");
            }

            string message1 = await _shopService.UpdateShopName(idListUser[1], "we", "Новое имя");
            // тут меняем на новое имя через строку
            ConsoleLog(message1 + $"\n {idListUser[1]} we  Новое имя");
        }

    }

}
