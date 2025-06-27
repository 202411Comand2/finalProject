using BLL.Dto.Cart;

namespace BLL.Orders.Abstractions
{
    public interface ICartService
    {
        public Task<bool> AddNewCart(AddCartDto cartDto);

        public Task<bool> DeleteCart(DeleteCartDto cartDto);

        public Task<bool> UpdateCart(UpdateCartDto cartDto);

        public Task<List<CartDto>> GetAllCart(GetCartDto cartDto);
    }
}
