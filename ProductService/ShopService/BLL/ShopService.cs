using Domain.Entities;
using DAL.Repositories;
using DAL.Abstractions;
using BLL.Products.Abstractions;
using BLL.Dto.Shop;


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
        public async Task<int> CreateShop(AddShopDto addShopDto)
        {
            if (await _shopRepository.GetIdByStoreName(addShopDto.Name) != -1)
            {
                return -1;
            }
            Shop shop = new Shop
            {
                Name = addShopDto.Name,
                IsDelete = false,
            };
            return (await _shopRepository.Add(shop)).Id;
        }
       ////TODO как быть с удалением товаров??? если удалить из этого запроса, то мы далем монолит
        /// <summary>
        /// Удаление магазина
        /// </summary>
        /// <param name="shopID">ClusterId магазина, который нужно удалить></param>
        /// <returns>Магазин удалён или нет</returns>
        public async Task<bool> DeleteShop(DeleteShopDto shopDto)
        {
            Shop shop = await _shopRepository.Get(shopDto.Id);
            if (await _shopRepository.Get(shopDto.Id) is null)
            {
                return false;
            }
            else
            {
                shop.IsDelete = true;
                await _shopRepository.DeleteShopWithProducts(shop);
                await _shopRepository.Update(shop);
                return true;
            }
        }

        /// <summary>
        /// Обновление название магазина
        /// </summary>
        /// <param name="shopId">ClusterId магазина</param>
        /// <param name="newShopName">Название магазина</param>
        /// <returns>Удалось ли обновить магазин</returns>
        public async Task<bool> UpdateNameShop(UpdateShopDto addShopDto)
        {
            Shop shop = await _shopRepository.Get(addShopDto.Id);
            if (string.IsNullOrEmpty(addShopDto.NewName))
            {//название null или пустое
                return false; 
            }
            if (shop is null)
            { //  В бд такого магазина нет
                return false;
            }
            else
            {
                if (await _shopRepository.GetIdByStoreName(addShopDto.NewName) == -1)
                {
                    shop.Name= addShopDto.NewName;
                    await _shopRepository.Update(shop);
                    return true;
                }
                else
                { // название занято
                    return false;
                }
            }
        }


        public async Task<List<ShopDto>> GetShopsInfo(GetShopsInfoDto shopDto) 
        {
            if(shopDto.ShopIds.Count==0)
            { 
                return null;
            }
            var sss = await _shopRepository.GetShopsByIds(shopDto.ShopIds);
            return Adapters.ShopAdapter.ConvertFromToEntityShopDto(sss);
        }
    }
}
