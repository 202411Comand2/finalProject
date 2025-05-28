using BLL.Dto;
using BLL.Dto.Shop;

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
        /// <param name="shopDto">Достаточно передать название магазина </param>
        /// <returns>Возвращает созданный магазин</returns>
        public Task<AnswerWithBackendDto<ShopDto>> CreateShop(AddShopDto addShopDto);

        /// <summary>
        /// Обновить название магазина
        /// </summary>
        /// <param name="shopId">Достаточно передать название и ClusterId магазина</param>
        /// <returns>Удалось ли обновить магазин</returns>
        public Task<AnswerWithBackendDto<ShopDto>> UpdateNameShop(UpdateShopDto shopDto);

        /// <summary>
        /// Удаление магазина
        /// </summary>
        /// <param name="shopDto"> Достаточно передать ClusterId магазина, который нужно удалить</param>
        /// <returns>Удалось ли удалить магазин</returns>
        public Task<AnswerWithBackendDto<ShopDto>> DeleteShop(DeleteShopDto shopDto);

        /// <summary>
        /// Восстановить магазин
        /// <param name="restoreShop"></param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ShopDto>> RestoreStore(RestoreShopDto restoreShop);

        /// <summary>
        /// Получить список магазинов по id 
        /// </summary>
        /// <param name="shopDto">Коллекция id магазинов</param>
        /// <returns></returns>
        public Task<AnswerWithBackendDto<ShopDto>> GetShopsInfo(GetShopsInfoDto shopDto);
    }
}
