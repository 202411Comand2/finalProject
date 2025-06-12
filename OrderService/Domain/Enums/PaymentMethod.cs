namespace Domain.Enums
{
    /// <summary>
    /// Способ оплаты
    /// </summary>
    public enum PaymentMethod
    {
        /// <summary>
        /// Кредитная карта
        /// </summary>
        CreditCard = 0,

        /// <summary>
        /// Наличные
        /// </summary>
        Cash = 1,

        /// <summary>
        /// Наложенный платеж
        /// </summary>
        CashOnDelivery = 2,

        /// <summary>
        /// Электронный кошелек
        /// </summary>
        ElectronicWallet = 3,            
    }
}
