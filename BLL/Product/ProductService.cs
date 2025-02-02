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
       // private readonly ShopOwnerRepository _shopOwnerRepository;
        private readonly UserRepository _userRepository;

        /// <summary>
        /// Конструктор класса 
        /// </summary>
        /// <param name="contextManager"></param>
        public ProductService(IContextManager contextManager)
        {
            _shopRepository = new ShopRepository(contextManager);
         //   _shopOwnerRepository = new ShopOwnerRepository(contextManager);
            _userRepository = new UserRepository(contextManager);
        }

        /// <summary>
        /// Добавить магазин
        /// </summary>
        /// <param name="name">Имя магазина</param>
        /// <param name="ShopOwner">Владелец магазина</param>
        /// <returns></returns>
        public async Task<string> CreateShop(string name)
        {
            Domain.Entities.Shop newShop = new Domain.Entities.Shop
            {
                Name = name,
                IsDelete = false,
            };
            var result = await _shopRepository.Add(newShop);
            return "магазин создан";
        }


        /// <summary>
        /// Обновить название магазина
        /// </summary>
        /// <param name="idShop">id магазина</param>
        /// <param name="newNameShop">Новое название магазина</param>
        /// <returns></returns>
        public async Task<string> UpdateShopName( int idShop, string newNameShop)
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



    }
}
