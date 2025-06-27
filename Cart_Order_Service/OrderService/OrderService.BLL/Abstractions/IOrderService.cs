using OrderService.BLL.Dto.Order;
using SupperBackEnd.Dto;

namespace OrderService.BLL.Abstractions
{
    public interface IOrderService
    {
        /// <summary>
        /// Создание заказа
        /// </summary>
        Task<AnswerWithBackendDto<OrderDto>> AddOrderAsync(
            AddOrderDto dto, 
            CancellationToken cancellationToken = default);
        /// <summary>
        /// Получить все заказы пользователя
        /// </summary>
        Task<AnswerWithBackendDto<OrderDto>> GetAllOrderUserAsync(
            GetAllOrderDto dto, 
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Получить один заказ пользователя
        /// </summary>
        Task<AnswerWithBackendDto<OrderDto>> GetOrderUserAsync(
            GetOrderDto dto, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновление состояния заказа
        /// </summary>
        Task<AnswerWithBackendDto<OrderDto>> UpdateOrderStatusAsync(
            UpdateOrderDto dto, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Удалить заказ пользователя
        /// </summary>
        Task<AnswerWithBackendDto<OrderDto>> DeleteOrderAsync(
            DeleteOrderDto dto, 
            CancellationToken cancellationToken = default);
    }
}
