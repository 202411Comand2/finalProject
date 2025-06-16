using OrderService.BLL.Abstractions;
using OrderService.BLL.Dto.Order;
using OrderService.DAL.Abstractions;
using OrderService.DAL.Repositories;
using OrderService.Domain.Entities;
//using Rabbit.Platform;
using SupperBackEnd.Dto;

namespace OrderService.BLL
{
    public class OrderService(IContextManager contextManager//,
        //IMessagePublisher messagePublisher
        ) : IOrderService
    {
        private readonly OrderRepository _orderRepository = new OrderRepository(contextManager);
        //private readonly IMessagePublisher _messagePublisher = messagePublisher;

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

                    /*dto.OrderDetail.ForEach(x =>
                    {
                        var detail = new OrderDetail()
                        {
                            //OrderId = order.Id,
                            ProductId = x.ProductId,
                            Price = x.Price,
                            Count = x.Count,
                        };

                        order.OrderDetailId.Add(detail);
                    });*/

                    cancellationToken.ThrowIfCancellationRequested();

                    answerWithBackendDto.AddObject(
                        Adapters.OrderAdapter.ConvertFromEntitieToDTO(
                            await _orderRepository.Add(order))
                    );

                    /*answerWithBackendDto.AddObject(
                        Adapters.OrderAdapter.ConvertFromEntitieToDTO(
                            await _orderRepository.Add(order.OrderDetailId))
                    );*/

                    return answerWithBackendDto;
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
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
                        Adapters.OrderAdapter.ConvertFromEntitieToDTO(
                            await _orderRepository.Add(order))
                        );

                    return answerWithBackendDto;
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }

        /// <summary>
        /// Получить заказы пользователя
        /// </summary>
        public async Task<AnswerWithBackendDto<OrderDto>> GetAllOrderUserAsync(GetAllOrderDto dto, CancellationToken cancellationToken)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<OrderDto> answerWithBackendDto = new();

                    var items = Adapters.OrderAdapter.ConvertFromEntitieToDTO(
                        await _orderRepository.GetListOrders(dto.IdUser));

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
            });
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

                    var items = Adapters.OrderAdapter.ConvertFromEntitieToDTO(
                        await _orderRepository.GetListOrders(dto.IdOrder));

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
            });
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

        /*/// <summary>
        /// Отправка сообщения в RabbitMQ о том, что статус был изменен
        /// </summary>
        private async void SendMessageToRabbitAsync(int shopId, string routingKey)
        {
            await _messagePublisher.SendMessageAsync<ShopChangeMessage>(new ShopChangeMessage(shopId), routingKey, "shop.exchange");
        }*/
    }
}
