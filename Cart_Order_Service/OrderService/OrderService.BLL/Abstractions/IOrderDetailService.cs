using OrderService.BLL.Dto.OrderDetail;
using SupperBackEnd.Dto;

namespace OrderDetailService.BLL.Abstractions
{
    public interface IOrderDetailDetailService
    {
        /// <summary>
        /// Создание детализации заказа
        /// </summary>
        Task<AnswerWithBackendDto<OrderDetailDto>> AddOrderDetailAsync(
            AddOrderDetailDto dto, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Получить детализацию заказа
        /// </summary>
        Task<AnswerWithBackendDto<OrderDetailDto>> GetAllOrderDetailUserAsync(
            GetOrderDetailDto dto, 
            CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Обновление статуса позиции
        /// </summary>
        Task<AnswerWithBackendDto<OrderDetailDto>> UpdateOrderDetailStatusAsync(
            UpdateOrderDetailDto dto, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Удалить детализацию
        /// </summary>
        Task<AnswerWithBackendDto<OrderDetailDto>> DeleteOrderDetailAsync(
            DeleteOrderDetailDto dto, 
            CancellationToken cancellationToken = default);
    }
}
