using CartService.BLL.Dto;
using SupperBackEnd.Dto;

namespace CartService.BLL.Abstractions
{
    public interface ICartService
    {

        /// <summary>
        /// Добавить товар в корзину
        /// </summary>
        public Task<AnswerWithBackendDto<CartDto>> AddCartProductAsync(AddCartDto dto, CancellationToken token = default);

        /// <summary>
        /// Удалить товар из корзины
        /// </summary>
        public Task<AnswerWithBackendDto<CartDto>> DeleteProductAsync(DeleteCartDto dto, CancellationToken token = default);

        /// <summary>
        /// Получить корзину пользователя
        /// </summary>
        public Task<AnswerWithBackendDto<CartDto>> GetCartUserAsync(GetCartDto dto, CancellationToken token = default);

        /// <summary>
        /// Обновить товар в корзине
        /// </summary>
        public Task<AnswerWithBackendDto<CartDto>> UpdateProductAsync(UpdateCartDto dto, CancellationToken token = default);

        
    }
}
