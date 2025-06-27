namespace OrderService.Domain.Enums
{
    public class StatusPosition
    {
        /// <summary>
        /// Состояние позиции в заказе
        /// </summary>
        public enum OrderStatus
        {
            /// <summary>
            /// Создан
            /// </summary>
            Create = 0,

            /// <summary>
            /// Возвращен
            /// </summary>
            Returned = 1,

            /// <summary>
            /// Утерян
            /// </summary>
            Lost = 2,

            /// <summary>
            /// Брак
            /// </summary>
            Defective = 3,
        }
    }
}
