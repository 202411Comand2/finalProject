using OrderService.BLL.Dto.Order;
using OrderService.Domain.Entities;
using SupperBackEnd.Dto;

namespace OrderService.BLL.Abstractions
{
    public interface IOrderService
    {
        /// <summary>
        /// Создание заказа
        /// </summary>
        Task<AnswerWithBackendDto<AddOrderDto>> AddOrder(AddOrderDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить все заказы пользователя
        /// </summary>
        Task<AnswerWithBackendDto<OrderDto>> GetAllOrderUserAsync(int userId, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Получить один заказ пользователя
        /// </summary>
        Task<AnswerWithBackendDto<GetOrderDto>> GetOrderUserAsync(GetOrderDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновление состояния заказа
        /// </summary>
        Task<AnswerWithBackendDto<UpdateOrderDto>> UpdateOrderStatusAsync(UpdateOrderDto dto, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удалить заказ пользователя
        /// </summary>
        Task<AnswerWithBackendDto<DeleteOrderDto>> DeleteOrderAsync(DeleteOrderDto dto, CancellationToken cancellationToken = default);
    }
}
