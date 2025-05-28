using BLL.Dto.Cart;
using BLL.Orders.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using DAL.Abstractions;

namespace BLL.Orders
{
    public class CartService : ICartService
    {
        private readonly CartRepository _cartRepository;
        public CartService(IContextManager contextManager) => _cartRepository = new CartRepository(contextManager);
        public async Task<bool> AddNewCart(AddCartDto cartDto)
        {
            Cart cartEntity = Adapters.CartAdapter.ConvertFromDtoCartToEntity(cartDto);
            if ((await _cartRepository.Add(cartEntity)) is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteCart(DeleteCartDto cartDto)
        {
            Cart cartEntity = await _cartRepository.Get(cartDto.Id);
            if (cartEntity == null)
            {
                return false;

            }
            await _cartRepository.Update(cartEntity);
            return true;
        }
        public async Task<bool> UpdateCart(UpdateCartDto cartDto)
        {
            Cart cartEntity = Adapters.CartAdapter.ConvertFromDtoCartToEntity(cartDto);
            Cart cartNew = await _cartRepository.Get(cartEntity.Id);
            if (cartNew is null)
            {
                return false;
            }
            cartEntity.Count = cartNew.Count;
            await _cartRepository.Update(cartEntity);
            return true;
        }
        public async Task<List<CartDto>> GetAllCart(GetCartDto cartDto)
        {
            List<CartDto> carts = new List<CartDto>();
            foreach (Cart cart in await _cartRepository.GetUserCart(cartDto.UserId))
            {
                carts.Add(Adapters.CartAdapter.ConvertFromEntitieCartToDto(cart));
            }
            return carts;
        }
    }
}
