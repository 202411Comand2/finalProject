using BLL.Abstractions;
using BLL.Dto;
using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;
using SupperBackEndDto;
using System.Threading;

namespace BLL
{
    public class CartService : ICartService
    {
        private readonly CartRepository _cartRepository;
        public CartService(IContextManager contextManager) => _cartRepository = new CartRepository(contextManager);

        /// <summary>
        /// Добавление продукта в корзину или обновление количества
        /// </summary>
        public async Task<AnswerWithBackendDto<CartDto>> AddCartProduct(AddCartDto addCartDto, CancellationToken cancellationToken)
        { 
            return await Task.Run(async () =>
            {
               try
                {
                    AnswerWithBackendDto<CartDto> answerWithBackendDto = new();
                    Cart cart;

                    var userCarts = await _cartRepository.GetListProducts(addCartDto.UserId);
                    var userCart = userCarts.Where(x => x.ProductId == addCartDto.ProductId).FirstOrDefault();

                    cancellationToken.ThrowIfCancellationRequested();
                    if (userCart is not null)
                    {
                        cart = await _cartRepository
                            .UpdateCountProductInCart(addCartDto.UserId, addCartDto.ProductId, userCart.Count + addCartDto.Count);
                    }
                    else
                    {
                        cart = new Cart()
                        {
                            UserId = addCartDto.UserId,
                            ProductId = addCartDto.ProductId,
                            Count = addCartDto.Count,
                        };
                    }

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
        public async Task<AnswerWithBackendDto<CartDto>> DeleteProduct(int id, CancellationToken token)
        {
            return await Task.Run(async () => 
            {
                try 
                { 
                    AnswerWithBackendDto<CartDto> answerWithBackendDto = new();
                    Cart product = await _cartRepository.Get(id);
                    if (product is not null)
                    {
                        answerWithBackendDto.AddErrorLog("Ошибка. Не найдена данныая позиция");
                        return answerWithBackendDto;
                    }

                    await _cartRepository.Delete(product);
                    answerWithBackendDto.DataReceived = true;
                    answerWithBackendDto.ObjectDto = null;

                    return answerWithBackendDto;
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }

        /// <summary>
        /// Получить корзину пользователя
        /// </summary>
        public async Task<AnswerWithBackendDto<CartDto>> GetCartUser(int userId, CancellationToken token)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<CartDto> answerWithBackendDto = new();

                    var items = Adapters.CartAdapter.ConvertFromEntityToCartDto(await _cartRepository.GetListProducts(userId));

                    if (items is null)
                    {
                        answerWithBackendDto.AddErrorLog("Ошибка получения корзины пользователя");
                        return answerWithBackendDto;
                    }

                    answerWithBackendDto.AddObject(Adapters.CartAdapter.ConvertFromEntityToCartDto
                        (await _cartRepository.Get(userId)));

                    return answerWithBackendDto;
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }
    }
}
