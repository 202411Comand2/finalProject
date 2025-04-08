using BLL.Dto;
using BLL.Dto.Favorite;
using BLL.Products.Abstractions;
using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
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

        public async Task<AnswerWithBackendDto<FavoriteDto>> AddFavoriteProduct(AddFavoriteDto addFavoriteDto)
        {
            AnswerWithBackendDto<FavoriteDto> answerWithBackendDto = new();
            if (await _favoriteRepository.GetFavoriteUser(addFavoriteDto.UserId, addFavoriteDto.ProductId) is not null)
            {
                answerWithBackendDto.AddErrorLog("Ошибка. Указанная позиция в избранном уже состоит");
                return answerWithBackendDto;
            }
            var _favorite = new Favorite()
            {
                UserId = addFavoriteDto.UserId,
                IdProduct = addFavoriteDto.ProductId,
            };
            answerWithBackendDto.AddObject(
                Adapters.FavoriteAdapter.ConvertFromEntityToFavoriteDto(
                    await _favoriteRepository.Add(_favorite)
                    )
                );
            return answerWithBackendDto;// "Продукт прикреплён к магазину и кластеру добавлен";
        }

        public async Task<AnswerWithBackendDto<FavoriteDto>> DeleteFavoriteProduct(DeleteFavoriteDto Dto)
        {
            AnswerWithBackendDto<FavoriteDto> answerWithBackendDto = new();
            Favorite favorite = await _favoriteRepository.Get(Dto.IdFavorite);
            if (favorite is not null)
            {
                answerWithBackendDto.AddErrorLog("Ошибка. Удалить не польчилось из-за того, что данная позиция уже не в избранном");
                return answerWithBackendDto;
            }
            await _favoriteRepository.Delete(favorite);
            answerWithBackendDto.DataReceived = true;
            answerWithBackendDto.ObjectDto = null;
            return answerWithBackendDto;// "Удалалил из избранного";
        }

        public async Task<AnswerWithBackendDto<FavoriteDto>> GetFavoriteUser(GetFavoriteDto getFavoriteDto)
        {
            AnswerWithBackendDto<FavoriteDto> answerWithBackendDto = new();
            var items = Adapters.FavoriteAdapter.ConvertFromEntityToFavoriteDto
                (await _favoriteRepository.GetFavoritesUser(getFavoriteDto.IdUser));
            if (items.Count==0) 
            {
                answerWithBackendDto.AddErrorLog("Ошибка. Отсутвуют позиции.");
                return answerWithBackendDto;
            }
            answerWithBackendDto.AddObject(Adapters.FavoriteAdapter.ConvertFromEntityToFavoriteDto
                (await _favoriteRepository.GetFavoritesUser(getFavoriteDto.IdUser)));
            return answerWithBackendDto;
        }
    }
}
