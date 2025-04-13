using Domain.Entities;
using DAL.Repositories;
using DAL.Abstractions;
using BLL.Products.Abstractions;
using BLL.Dto.Shop;
using BLL.Dto;


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
        public async Task<AnswerWithBackendDto<ShopDto>> CreateShop(AddShopDto addShopDto)
        {
            AnswerWithBackendDto<ShopDto> answerWithBackendDto = new();

            if (await _shopRepository.GetIdByStoreName(addShopDto.Name) != -1)
            {
                answerWithBackendDto.AddErrorLog("Не получилось создать магазин. Данное имя занято в системе!");
                return answerWithBackendDto;
            }
            Shop shop = new Shop
            {
                Name = addShopDto.Name,
                IsDelete = false,
            };
            answerWithBackendDto.AddObject(Adapters.ShopAdapter.ConvertFromEntitieToDTO(await _shopRepository.Add(shop)));
            return answerWithBackendDto;
        }

        ////TODO как быть с удалением товаров??? если удалить из этого запроса, то мы далем монолит
        /// <summary>
        /// Удаление магазина
        /// </summary>
        /// <param name="shopID">ClusterId магазина, который нужно удалить></param>
        /// <returns>Магазин удалён или нет</returns>
        public async Task<AnswerWithBackendDto<ShopDto>> DeleteShop(DeleteShopDto shopDto)
        {
            AnswerWithBackendDto<ShopDto> answerWithBackendDto = new();

            Shop shop = await _shopRepository.Get(shopDto.Id);

            if (await _shopRepository.Get(shopDto.Id) is null)
            {
                answerWithBackendDto.AddErrorLog($"Магазин, который вы пытаетесь удалить по id = {shopDto.Id}, не сущуствует или принадлежит не вам!");
                return answerWithBackendDto;
            }
            if (shop.IsDelete)
            {
                answerWithBackendDto.AddErrorLog($"Магазин, который вы пытаетесь удалить по id = {shopDto.Id}, не сущуствует или принадлежит не вам!");
                return answerWithBackendDto;
            }
            shop.IsDelete = true;
            // await _shopRepository.DeleteShopWithProducts(shop);
            answerWithBackendDto.AddObject(Adapters.ShopAdapter.ConvertFromEntitieToDTO(await _shopRepository.Update(shop)));
            return answerWithBackendDto;
        }

        /// <summary>
        /// Обновление название магазина
        /// </summary>
        /// <param name="shopId">ClusterId магазина</param>
        /// <param name="newShopName">Название магазина</param>
        /// <returns>Удалось ли обновить магазин</returns>
        public async Task<AnswerWithBackendDto<ShopDto>> UpdateNameShop(UpdateShopDto addShopDto)
        {
            AnswerWithBackendDto<ShopDto> requst = new();
            Shop shop = await _shopRepository.Get(addShopDto.Id);

            if (string.IsNullOrEmpty(addShopDto.NewName))
            {//название null или пустое
                requst.AddErrorLog("Новое имя, которое вы задали пустое!");
                return requst;
            }
            if (shop is null)
            { //  В бд такого магазина нет
                requst.AddErrorLog("По указанному Вами id не удалось найти магазин, принадлежащий Вам!");
                return requst;
            }

            if (shop.Name == addShopDto.NewName)
            {
                requst.AddErrorLog("Указанное вами новое название магазина = старому!");
                return requst;
            }

            if (await _shopRepository.GetIdByStoreName(addShopDto.NewName) == -1)
            {
                shop.Name = addShopDto.NewName;
                requst.AddObject(Adapters.ShopAdapter.ConvertFromEntitieToDTO(await _shopRepository.Update(shop)));
                return requst;
            }
            else
            { // название занято
                requst.AddErrorLog("Указанное вами новое имя магазина занято!");
                return requst;
            }
        }


        public async Task<AnswerWithBackendDto<ShopDto>> GetShopsInfo(GetShopsInfoDto shopDto)
        {
            AnswerWithBackendDto<ShopDto> answerWithBackendDto = new();

            if (shopDto.ShopIds.Count == 0)
            {
                answerWithBackendDto.AddErrorLog("Была введена пустая коллекция");
                return answerWithBackendDto;
            }
            var collectionShop = await _shopRepository.GetShopsByIds(shopDto.ShopIds);
            if (collectionShop.Count == 0) 
            {
                answerWithBackendDto.AddErrorLog("По указанному массиву id не удалось найти магазины");
                return answerWithBackendDto;
            }
            answerWithBackendDto.AddObject(Adapters.ShopAdapter.ConvertFromToEntityShopDto(collectionShop));
            return answerWithBackendDto;
        }

        public async Task<AnswerWithBackendDto<ShopDto>> RestoreStore(RestoreShopDto restoreShop)
        {
            AnswerWithBackendDto<ShopDto> answerWithBackendDto = new();

            Shop shop = await _shopRepository.Get(restoreShop.Id);

            if (await _shopRepository.Get(restoreShop.Id) is null)
            {
                answerWithBackendDto.AddErrorLog($"Магазин, который вы пытаетесь восстановить по id = {restoreShop.Id}, не существует или принадлежит не вам!");
                return answerWithBackendDto;
            }
            if (!shop.IsDelete)
            {
                answerWithBackendDto.AddErrorLog($"Магазин, который вы пытаетесь восстановить по id = {restoreShop.Id}, не удалён!");
                return answerWithBackendDto;
            }
            shop.IsDelete = false;
            // await _shopRepository.DeleteShopWithProducts(shop);
            answerWithBackendDto.AddObject(Adapters.ShopAdapter.ConvertFromEntitieToDTO(await _shopRepository.Update(shop)));
            return answerWithBackendDto;
        }


        private async Task DeleteProduct() 
        {
        
        }
    }
}
