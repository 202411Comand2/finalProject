using BLL.Dto.Favorite;
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

        public async Task<bool> AddFavoriteProduct(AddFavoriteDto addFavoriteDto)
        {
            if (await _favoriteRepository.GetFavoriteUser(addFavoriteDto.UserId, addFavoriteDto.ProductId) is not null)
            {
                return false;// "Ошибка. Указанная позиция в избранном уже состоит";
            }
            var _favorite = new Favorite()
            {
                UserId = addFavoriteDto.UserId,
                IdProduct = addFavoriteDto.ProductId,
            };

            await _favoriteRepository.Add(_favorite);
            return true;// "Продукт прикреплён к магазину и кластеру добавлен";
        }

        public async Task<bool> DeleteFavoriteProduct(DeleteFavoriteDto Dto)
        {
            Favorite favorite = await _favoriteRepository.Get(Dto.IdFavorite);
            await _favoriteRepository.Delete(favorite);
            return false;// "Удалалил из избранного";
        }
    }
}
