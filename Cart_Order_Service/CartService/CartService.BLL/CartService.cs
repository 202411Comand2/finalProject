using CartService.BLL.Abstractions;
using CartService.BLL.Adapters;
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
        public async Task<AnswerWithBackendDto<AddCartDto>> AddCartProductAsync(AddCartDto dto, CancellationToken cancellationToken)
        { 
            return await Task.Run(async () =>
            {
               try
                {
                    AnswerWithBackendDto<AddCartDto> answerWithBackendDto = new();
                    var modelEntites = await _cartRepository.Add(CartAdapter.ConvertFromDTOToEntity(dto));
                    if (modelEntites is null)
                    {
                        answerWithBackendDto.AddErrorLog($"not create: {dto.UserId}");
                        return answerWithBackendDto;
                    }
                    else
                    {
                        answerWithBackendDto.AddObject(CartAdapter.ConvertFromEntitieToDTO(modelEntites));
                        return answerWithBackendDto;
                    }
                    throw new NotImplementedException();
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
        public async Task<AnswerWithBackendDto<UpdateCartDto>> UpdateProductAsync(UpdateCartDto dto, CancellationToken cancellationToken)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<UpdateCartDto> answerWithBackendDto = new();

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

                    answerWithBackendDto.AddObject(CartAdapter.ConvertFromEntityToCartDto(await _cartRepository.Add(product)));

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
