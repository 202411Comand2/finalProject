namespace Rabbit.Platform.Enums
{
    public enum RoutingKeyStatusOrder
    {
        /// <summary>
        /// Заказ создан
        /// </summary>
        OrderCreated,

        /// <summary>
        /// Заказ оплачен
        /// </summary>
        OrderPaidFor,

        /// <summary>
        /// Заказ отправлен в доставку
        /// </summary>
        OrderSentToDelivery,

        /// <summary>
        /// Заказ доставлен
        /// </summary>
        OrderDelivered,

        /// <summary>
        /// Заказ получен
        /// </summary>
        OrderReceived,

        /// <summary>
        /// Заказ возвращен
        /// </summary>
        OrderReturn
    }
}
