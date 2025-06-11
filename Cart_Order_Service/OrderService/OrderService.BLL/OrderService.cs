using OrderService.BLL.Abstractions;
using OrderService.BLL.Dto;
using OrderService.DAL.Abstractions;
using OrderService.DAL.Repositories;
using OrderService.Domain.Entities;
using OrderService.Domain.Enums;
using SupperBackEnd.Dto;

namespace OrderService.BLL
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(IContextManager contextManager) => _orderRepository = new OrderRepository(contextManager);

        /// <summary>
        /// Создание заказа
        /// </summary>
        public async Task<AnswerWithBackendDto<OrderDto>> AddOrder(CreateOrderDto addOrderDto, CancellationToken token)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<OrderDto> answerWithBackendDto = new();
                    Order order;

                    foreach (var item in addOrderDto.AddOrder)
                    {
                        order = new Order()
                        {
                            DateCreated = DateTime.Now,
                            Count = item.Count,
                            ArriveDate = DateTime.Now.AddDays(5),
                            UserId = item.UserId,
                            ProductId = item.ProductId,
                            ShippingMethod = item.ShippingMethod,
                            PaymentMethod = item.PaymentMethod,
                            OrderStatus = item.OrderStatus,
                            ArriveAddress = item.ArriveAddress,
                        };
                        answerWithBackendDto.AddObject(
                            Adapters.OrderAdapter.ConvertFromEntityToOrderDto(
                                await _orderRepository.Add(order))
                            );
                    }

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
        public async Task<AnswerWithBackendDto<OrderDto>> UpdateOrderStatus(int id, OrderStatus orderStatus, CancellationToken token)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<OrderDto> answerWithBackendDto = new();
                    Order order;

                    var userOrder = await _orderRepository.GetCurrentOrder(id);
                    if (userOrder is not null)
                    {
                        order = await _orderRepository.UpdateStatusAsync(id, orderStatus);
                    }

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
        public async Task<AnswerWithBackendDto<OrderDto>> GetOrderUser(int userId, CancellationToken token)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    AnswerWithBackendDto<OrderDto> answerWithBackendDto = new();

                    var items = Adapters.OrderAdapter.ConvertFromEntityToOrderDto(await _orderRepository.GetListOrders(userId));

                    if (items is null)
                    {
                        answerWithBackendDto.AddErrorLog("Ошибка получения заказов пользователя");
                        return answerWithBackendDto;
                    }

                    answerWithBackendDto.AddObject(Adapters.OrderAdapter.ConvertFromEntityToOrderDto
                        (await _orderRepository.Get(userId)));

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
