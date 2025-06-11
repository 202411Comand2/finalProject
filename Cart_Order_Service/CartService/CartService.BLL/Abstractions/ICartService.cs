using CartService.BLL.Dto;
using SupperBackEnd.Dto;

namespace CartService.BLL.Abstractions
{
    public interface ICartService
    {

        /// <summary>
        /// Добавить товар в корзину
        /// </summary>
        public Task<AnswerWithBackendDto<CartDto>> AddCartProduct(AddCartDto addCartDto, CancellationToken token = default);

        /// <summary>
        /// Удалить товар из корзины
        /// </summary>
        public Task<AnswerWithBackendDto<CartDto>> DeleteProduct(int id, CancellationToken token = default);

        /// <summary>
        /// Получить корзину пользователя
        /// </summary>
        public Task<AnswerWithBackendDto<CartDto>> GetCartUser(int userId, CancellationToken token = default);


    }
}
