using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DAL.Repositories;
using DAL.Abstractions;
using System.Xml.Linq;
using BLL.Products.Abstractions;


namespace BLL.Product
{
    /// <summary>
    /// Сервис по работе с магазином
    /// </summary>
    public class ShopService : IShopService
    {
        private readonly ShopRepository _shopRepository;
        public ShopService(IContextManager contextManager)
        {
            _shopRepository = new ShopRepository(contextManager);
        }
        public async Task<Shop?> CreateShop(string shopeName)
        {
            if (await _shopRepository.GetIdByStoreName(shopeName) != -1)
            {
                return null;
            }
            Shop newShop = new()
            {
                Name = shopeName,
                IsDelete = false,
            };
            return await _shopRepository.Add(newShop);
        }
       ////TODO как быть с удалением товаров??? если удалить из этого запроса, то мы далем монолит
        /// <summary>
        /// Удаление магазина
        /// </summary>
        /// <param name="shopID">Id магазина, который нужно удалить></param>
        /// <returns>Магазин удалён или нет</returns>
        public async Task<bool> DeleteShop(int shopID)
        {
            Shop? shop = await _shopRepository.Get(shopID);
            if (shop is null)
            {
                return false;
            }
            else
            {
                shop.IsDelete = true;
                await _shopRepository.DeleteShopWithProducts(shop);
                return true;
            }
        }

        /// <summary>
        /// Обновление название магазина
        /// </summary>
        /// <param name="shopId">Id магазина</param>
        /// <param name="newShopName">Название магазина</param>
        /// <returns>Удалось ли обновить магазин</returns>
        public async Task<bool> UpdateNameShop(int shopId, string newShopName)
        {
            if (string.IsNullOrEmpty(newShopName))
            {//название null или пустое
                return false; 
            }
            Shop? shopUser = await _shopRepository.Get(shopId);
            if (shopUser is null)
            { //  В бд такого магазина нет
                return false;
            }
            else
            {
                if (await _shopRepository.GetIdByStoreName(newShopName) == -1)
                {
                    string oldNameShop = shopUser.Name;
                    shopUser.Name = newShopName;
                    await _shopRepository.Update(shopUser);
                    return true;
                }
                else
                { // название занято
                    return false;
                }
            }
        }
    }
}
