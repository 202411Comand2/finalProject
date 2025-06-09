
namespace Rabbit.Platform
{
    /// <summary>
    /// Сообщение очереди об изменении магазина.
    /// </summary>
    public class ShopChangeMessage : IMessage
    {
        /// <inheritdoc />
        public Guid Guid { get; set; } = Guid.NewGuid();

        /// <inheritdoc />
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Идентификатор магазина.
        /// </summary>
        public int ShopId { get; set; }

        public ShopChangeMessage(int shopId)
        {
            ShopId = shopId;
        }
    }
}