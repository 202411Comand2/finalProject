using Rabbit.Platform;
using ShopService.DAL;
using ShopService.Domain;
using SupperBackEnd.Dto;

namespace ShopService.BLL
{
    /// <summary>
    /// Сервис по работе с магазином
    /// </summary>
    public class ShopMainService : IShopMainService
    {
        private readonly ShopRepository _shopRepository;
        private readonly IMessagePublisher _messagePublisher;

        public ShopMainService(IContextManager contextManager, 
            IMessagePublisher messagePublisher)
        {
            _shopRepository = new ShopRepository(contextManager);
            _messagePublisher = messagePublisher;
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
                Adress = addShopDto.Adress,
                ContactInfo = addShopDto.ContactInfo,
                Description = addShopDto.Description,
                IsDelete = false,
            };
            answerWithBackendDto.AddObject(ShopAdapter.ConvertFromEntitieToDTO(await _shopRepository.Add(shop)));
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
            answerWithBackendDto.AddObject(ShopAdapter.ConvertFromEntitieToDTO(await _shopRepository.Update(shop)));

            SendMessageToRabbitAsync(shop.Id, RoutingKeys.ShopDeleted.ToString());

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
           
            if (string.IsNullOrEmpty(addShopDto.Name))
            {//название null или пустое
                requst.AddErrorLog("Новое имя, которое вы задали пустое!");
                return requst;
            }

            if (shop.Name != addShopDto.Name)
            {
                if (await _shopRepository.CheckNameShop(addShopDto.Name)) 
                {
                    requst.AddErrorLog("Новое имя, которое вы задали занято!");
                    return requst;
                }
            }
                requst.AddObject(ShopAdapter.ConvertFromEntitieToDTO(await _shopRepository.Update(shop)));
                return requst;
           
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
            answerWithBackendDto.AddObject(ShopAdapter.ConvertFromEntitieToDTO(await _shopRepository.Update(shop)));
            return answerWithBackendDto;
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
            answerWithBackendDto.AddObject(ShopAdapter.ConvertFromToEntityShopDto(collectionShop));
            return answerWithBackendDto;
        }

        public async Task<AnswerWithBackendDto<ShopDto>> GetShopsInfo(int id)
        {
            AnswerWithBackendDto<ShopDto> answerWithBackendDto = new();

            var shop = await _shopRepository.Get(id);
            if (shop is null)
            {
                answerWithBackendDto.AddErrorLog("По указанному массиву id не удалось найти магазины");
                return answerWithBackendDto;
            }
            answerWithBackendDto.AddObject(ShopAdapter.ConvertFromToEntityShopDto(shop));
            return answerWithBackendDto;
        }

        /// <summary>
        /// Отправка сообщения в RabbitMQ о том, что магазин был удалён.
        /// </summary>
        /// <param name="shopId">Идентификатор магазина</param>
        /// <param name="routingKey">Ключ маршрутизации сообщения.</param>
        private async void SendMessageToRabbitAsync(int shopId, string routingKey)
        {
            await _messagePublisher.SendMessageAsync<ShopChangeMessage>(new ShopChangeMessage(shopId), routingKey, "shop.exchange");
        }
    }
}
