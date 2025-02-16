using BLL.Products.Abstractions;
using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Products
{
    public class FavoriteProductService:IFavoriteProductService
    {
        private readonly FavoriteRepository _favoriteRepository;
        public FavoriteProductService(IContextManager contextManager) => _favoriteRepository = new FavoriteRepository(contextManager);

        public async Task<bool> AddFavoriteProduct(int userId, int productId)
        {
            if (await _favoriteRepository.GetFavoriteUser(userId, productId) is not null)
            {
                return false;// "Ошибка. Указанная позиция в избранном уже состоит";
            }
            var _favorite = new Favorite()
            {
                UserId = userId,
                IdProduct = productId,
            };

            await _favoriteRepository.Add(_favorite);
            return true;// "Продукт прикреплён к магазину и кластеру добавлен";
        }

        public async Task<bool> DeleteFavoriteProduct(int favoriteId)
        {
            Favorite favorite = await _favoriteRepository.Get(favoriteId);
            await _favoriteRepository.Delete(favorite);
            return false;// "Удалалил из избранного";
        }
    }
}
