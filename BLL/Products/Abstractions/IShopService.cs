using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Products.Abstractions
{
    /// <summary>
    /// Интерфейс магазина
    /// </summary>
    public interface IShopService
    {

        /// <summary>
        /// Создание магазина
        /// </summary>
        /// <param name="nameShope">Название магазина</param>
        /// <returns>Возращает созданный магазин</returns>
        public Task<Shop?> CreateShop(string shopeName);

        /// <summary>
        /// Обновить название магазина
        /// </summary>
        /// <param name="shopId">Id магазина</param>
        /// <returns>Удалось ли обновить магазин</returns>
        public Task<bool> UpdateNameShop(int shopId, string newShopName);

        /// <summary>
        /// Удаление магазина
        /// </summary>
        /// <param name="shopID">Id магазина, который нужно удалить</param>
        /// <returns>Удалось ли удалить магазин</returns>
        public Task<bool> DeleteShop(int shopID);
    }
}
