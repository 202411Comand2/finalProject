using CartService.BLL.Abstractions;
using CartService.BLL.Dto;
using CartService.DAL.Abstractions;
using CartService.DAL.Repositories;
using CartService.Domain.Entities;
using SupperBackEnd.Dto;

namespace CartService.BLL
{
    public class CartService : ICartService
    {
        private readonly CartRepository _cartRepository;
        public CartService(IContextManager contextManager) => _cartRepository = new CartRepository(contextManager);

        /// <summary>
        /// Добавление продукта в корзину
        /// </summary>
        public async Task<AnswerWithBackendDto<CartDto>> AddCartProductAsync(AddCartDto addCartDto, CancellationToken cancellationToken)
        { 
            return await Task.Run(async () =>
            {
               try
                {
                    AnswerWithBackendDto<CartDto> answerWithBackendDto = new();
                    Cart cart = new Cart();

                    var userCarts = await _cartRepository.GetListProducts(addCartDto.UserId);
                    var userCart = userCarts.Where(x => x.ProductId == addCartDto.ProductId).FirstOrDefault();

                    if (userCart is null)
                    {
                        cart.UserId = addCartDto.UserId;
                        cart.ProductId = addCartDto.ProductId;
                        cart.Count = addCartDto.Count;
                    }

                    cancellationToken.ThrowIfCancellationRequested();

                    answerWithBackendDto.AddObject(
                        Adapters.CartAdapter.ConvertFromEntityToCartDto(
                            await _cartRepository.Add(cart))
                        );

                    return answerWithBackendDto;
                }
                catch (Exception ex)
                {
                    throw;
                }
            },
            cancellationToken);
        }

        /// <summary>
        /// Удаление продукта из корзины
        /// </summary>
        public async Task<AnswerWithBackendDto<CartDto>> DeleteProductAsync(DeleteCartDto dto, CancellationToken cancellationToken)
        {
            return await Task.Run(async () => 
            {
                try 
                { 
                    AnswerWithBackendDto<CartDto> answerWithBackendDto = new();

                    Cart product = await _cartRepository.GetProductInCart(dto.IdCart);

                    if (product is null)
                    {
                        answerWithBackendDto.AddErrorLog("Ошибка. Не найдена данная позиция");
                        return answerWithBackendDto;
                    }

                    cancellationToken.ThrowIfCancellationRequested();

                    await _cartRepository.Delete(product);
                    answerWithBackendDto.DataReceived = true;
                    answerWithBackendDto.ObjectDto = null;

                    return answerWithBackendDto;
                }
                catch (Exception ex)
                {
                    throw;
                }
            },
            cancellationToken);
        }

        /// <summary>
        /// Получить корзину пользователя
        /// </summary>
        public async Task<AnswerWithBackendDto<CartDto>> GetCartUserAsync(GetCartDto dto, CancellationToken cancellationToken)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<CartDto> answerWithBackendDto = new();

                    var items = Adapters.CartAdapter.ConvertFromEntityToCartDto(
                        await _cartRepository.GetListProducts(dto.IdUser));

                    if (items.Count == 0)
                    {
                        answerWithBackendDto.AddErrorLog("Ошибка. Отсутвуют позиции.");
                        return answerWithBackendDto;
                    }

                    cancellationToken.ThrowIfCancellationRequested();

                    answerWithBackendDto.AddObject(items);

                    return answerWithBackendDto;
                }
                catch (Exception ex)
                {
                    throw;
                }
            },
            cancellationToken);
        }

        /// <summary>
        /// Обновить продукт в корзине
        /// </summary>
        public async Task<AnswerWithBackendDto<CartDto>> UpdateProductAsync(UpdateCartDto dto, CancellationToken cancellationToken)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<CartDto> answerWithBackendDto = new();

                    Cart product = await _cartRepository.GetProductInCart(dto.IdCart);

                    if (product is null)
                    {
                        answerWithBackendDto.AddErrorLog("Ошибка. Не найдена данная позиция");
                        return answerWithBackendDto;
                    }

                    cancellationToken.ThrowIfCancellationRequested();

                    await _cartRepository.Delete(product);

                    cancellationToken.ThrowIfCancellationRequested();

                    product!.Count += dto.Count;

                    answerWithBackendDto.AddObject(
                        Adapters.CartAdapter.ConvertFromEntityToCartDto(
                            await _cartRepository.Add(product))
                        );

                    return answerWithBackendDto;
                }
                catch (Exception ex)
                {
                    throw;
                }
            },
            cancellationToken);
        }
    }
}
