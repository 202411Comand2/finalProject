
using OrderService.Domain.Enums;

namespace Rabbit.Platform
{
    /// <summary>
    /// Сообщение очереди об изменении статуса заказа
    /// </summary>
    public class ChangeStatusOrderMessage : IMessage
    {
        /// <inheritdoc />
        public Guid Guid { get; set; } = Guid.NewGuid();

        /// <inheritdoc />
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public OrderStatus Status { get; set; }

        public ChangeStatusOrderMessage(int orderId)
        {
            OrderId = orderId;
        }
    }
}