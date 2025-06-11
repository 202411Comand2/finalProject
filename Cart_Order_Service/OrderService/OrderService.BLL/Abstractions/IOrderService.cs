using OrderService.BLL.Dto;
using OrderService.Domain.Enums;
using SupperBackEnd.Dto;

namespace OrderService.BLL.Abstractions
{
    public interface IOrderService
    {
        /// <summary>
        /// Создание заказа
        /// </summary>
        Task<AnswerWithBackendDto<OrderDto>> AddOrder(CreateOrderDto addOrderDto, CancellationToken token = default);

        /// <summary>
        /// Обновление состояния заказа
        /// </summary>
        Task<AnswerWithBackendDto<OrderDto>> UpdateOrderStatus(int id, OrderStatus orderStatus, CancellationToken token = default);

        /// <summary>
        /// Получить корзину пользователя
        /// </summary>
        Task<AnswerWithBackendDto<OrderDto>> GetOrderUser(int userId, CancellationToken token = default);
    }
}
