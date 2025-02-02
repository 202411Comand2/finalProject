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
        private ProductService _productService;
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
            _productService = new ProductService(cm);
            _userRepository = new UserRepository(cm);
            _shopRepository = new ShopRepository(cm);
            _shopOwnerRepository = new ShopOwnerRepository(cm);
        }
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
            string message = await _productService.CreateShop( $"Название магазина{rand.Next(0, 10_000)}");
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
                string message = await _productService.UpdateShopName( idListShop[i], newName[i]);
                ConsoleLog(message+$"\n idListShop {idListShop[i]} { newName[i]}");
            }

            string message1 = await _productService.UpdateShopName( "we", "Новое имя");
            // тут меняем на новое имя через строку
            ConsoleLog(message1 + $"\n  we  Новое имя");
        }

    }

}
