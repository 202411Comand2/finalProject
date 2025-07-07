using OrderService.BLL.Abstractions;
using OrderService.BLL.Adapters;
using OrderService.BLL.Dto.Order;
using OrderService.DAL.Abstractions;
using OrderService.DAL.Repositories;
using OrderService.Domain.Entities;
using Rabbit.Platform;
using SupperBackEnd.Dto;

namespace OrderService.BLL
{
    public class OrderService(IContextManager contextManager) : IOrderService
    {
        private readonly OrderRepository _orderRepository = new OrderRepository(contextManager);

        public async Task<AnswerWithBackendDto<OrderDto>> AddOrder(AddOrderDto dto, CancellationToken cancellationToken)
        {
            AnswerWithBackendDto<OrderDto> answerWithBackendDto = new();
            var modelEntites = await _orderRepository.Add(OrderAdapter.ConvertFromDTOToEntity(dto));
            if (modelEntites is null)
            {
                answerWithBackendDto.AddErrorLog($"not create: {dto.UserId}");
                return answerWithBackendDto;
            }
            else
            {
                answerWithBackendDto.AddObject(OrderAdapter.ConvertFromEntitieToDtoGet(modelEntites));
                return answerWithBackendDto;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        public async Task<AnswerWithBackendDto<OrderDto>> AddOrderAsync(AddOrderDto dto, CancellationToken cancellationToken)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<OrderDto> answerWithBackendDto = new();
                    var order = new Order()
                    {
                        DateCreated = DateTime.Now,
                        ArriveDate = DateTime.Now.AddDays(5),
                        UserId = dto.UserId,
                        ShippingMethod = dto.ShippingMethod,
                        PaymentMethod = dto.PaymentMethod,
                        OrderStatus = dto.OrderStatus,
                        ArriveAddress = dto.ArriveAddress,
                    };

                    cancellationToken.ThrowIfCancellationRequested();

                    answerWithBackendDto.AddObject(
                        OrderAdapter.ConvertFromEntitieToDtoGet(await _orderRepository.Add(order)));

                    return answerWithBackendDto;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Обновление состояния заказа
        /// </summary>
        public async Task<AnswerWithBackendDto<OrderDto>> UpdateOrderStatusAsync(UpdateOrderDto dto, CancellationToken cancellationToken)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<OrderDto> answerWithBackendDto = new();

                    var order = await _orderRepository.Get(dto.IdOrder);
                    
                    if (order is null)
                    {
                        answerWithBackendDto.AddErrorLog("Ошибка. Не найдена данная позиция");
                        return answerWithBackendDto;
                    }                    

                    cancellationToken.ThrowIfCancellationRequested();

                    await _orderRepository.Delete(order);

                    cancellationToken.ThrowIfCancellationRequested();

                    order!.OrderStatus = dto.OrderStatus;
                    order!.DateOrderStatus = DateTime.Now;

                    answerWithBackendDto.AddObject(
                        OrderAdapter.ConvertFromEntitieToDtoGet(await _orderRepository.Add(order)));

                    return answerWithBackendDto;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }, cancellationToken);
        }

        /// <summary>
        /// Получить заказы пользователя
        /// </summary>
        public async Task<AnswerWithBackendDto<OrderDto>> GetAllOrderUserAsync(int userId, CancellationToken cancellationToken)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<OrderDto> answerWithBackendDto = new();

                    var items = OrderAdapter.ConvertFromEntitieToDTO(
                        await _orderRepository.GetListOrders(userId));

                    if (items is null)
                    {
                        answerWithBackendDto.AddErrorLog("Ошибка получения заказов пользователя");
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
            }, cancellationToken);
        }

        /// <summary>
        /// Получить один заказ пользователя
        /// </summary>
        public async Task<AnswerWithBackendDto<OrderDto>> GetOrderUserAsync(GetOrderDto dto, CancellationToken cancellationToken)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<OrderDto> answerWithBackendDto = new();

                    var items = OrderAdapter.ConvertFromEntitieToDtoGet(
                        await _orderRepository.GetOrder(dto.IdOrder));

                    if (items is null)
                    {
                        answerWithBackendDto.AddErrorLog("Ошибка получения заказа пользователя");
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
            }, cancellationToken);
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        public async Task<AnswerWithBackendDto<OrderDto>> DeleteOrderAsync(DeleteOrderDto dto, CancellationToken cancellationToken)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<OrderDto> answerWithBackendDto = new();

                    Order order = await _orderRepository.GetOrder(dto.IdOrder);

                    if (order is null)
                    {
                        answerWithBackendDto.AddErrorLog("Ошибка. Не найдена данная позиция");
                        return answerWithBackendDto;
                    }

                    cancellationToken.ThrowIfCancellationRequested();

                    await _orderRepository.Delete(order);
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
    }
}
