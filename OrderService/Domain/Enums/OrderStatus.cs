namespace Domain.Enums
{
    /// <summary>
    /// Состояние заказа
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Создан
        /// </summary>
        Create = 0,

        /// <summary>
        /// Ожидает оплату
        /// </summary>
        AwaitingPayment = 1,

        /// <summary>
        /// Передан в доставку
        /// </summary>
        OnTheWay = 2,

        /// <summary>
        /// Выполнен
        /// </summary>
        Completed = 3,
    }
}
