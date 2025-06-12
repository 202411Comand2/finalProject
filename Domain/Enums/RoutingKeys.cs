namespace Rabbit.Platform
{
    /// <summary>
    /// Ключи маршрутизации для RabbitMQ.
    /// </summary>
    public enum RoutingKeys
    {
        /// <summary>
        /// Магазин создан.
        /// </summary>
        ShopCreated,
        
        /// <summary>
        /// Магазин обновлен.
        /// </summary>
        ShopUpdated,
        
        /// <summary>
        /// Магазин удален.
        /// </summary>
        ShopDeleted
    }
}