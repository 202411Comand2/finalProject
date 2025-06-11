using FavoriteService.DAL;
using FavoriteService.Domain;
using SupperBackEnd.Dto;

namespace FavoriteService.BLL
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
                FavoriteAdapter.ConvertFromEntityToFavoriteDto(
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
            var items = FavoriteAdapter.ConvertFromEntityToFavoriteDto
                (await _favoriteRepository.GetFavoritesUser(getFavoriteDto.IdUser));
            if (items.Count==0) 
            {
                answerWithBackendDto.AddErrorLog("Ошибка. Отсутвуют позиции.");
                return answerWithBackendDto;
            }
            answerWithBackendDto.AddObject(FavoriteAdapter.ConvertFromEntityToFavoriteDto
                (await _favoriteRepository.GetFavoritesUser(getFavoriteDto.IdUser)));
            return answerWithBackendDto;
        }
    }
}
