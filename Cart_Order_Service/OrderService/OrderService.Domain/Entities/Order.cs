using OrderService.Domain.Enums;
using Platform.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Domain.Entities
{
    /// <summary>
    /// Таблица заказов
    /// </summary>
    [Table("Order")]
    public class Order : IDbEntity
    {
        /// <summary>
        /// Id заказа
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        [Column("date_create")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата прибытия
        /// </summary>
        [Column("arrive_date")]
        public DateTime ArriveDate { get; set; } = DateTime.UtcNow.AddDays(5);

        /// <summary>
        /// Способ доставки
        /// </summary>
        [Column("shipping_method")]
        public int ShippingMethod { get; set; }

        /// <summary>
        /// Способ оплаты
        /// </summary>
        [Column("payment_method")]
        public int PaymentMethod { get; set; }

        /// <summary>
        /// Адрес доставки
        /// </summary>
        [Column("arrive_address")]
        public string ArriveAddress { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        [Column("OrderStatus")]
        public int OrderStatus { get; set; }

        /// <summary>
        /// Дата установки статуса заказа
        /// </summary>
        [Column("date_status")]
        public DateTime DateOrderStatus { get; set; } = DateTime.UtcNow;


        #region связи

        public int UserId { get; set; }

        #endregion

        /// <summary>
        /// Вернуть Id объекта
        /// </summary>
        /// <returns>Возвращает id объекта в бд</returns>
        public int GetPrimaryKey()
        {
            return Id;
        }
    }
}
