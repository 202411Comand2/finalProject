using Domain.Abstractions;
using FinalProjectEntityDataBase.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
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
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Состояние заказа
        /// </summary>
        [Column("state")]
        public OrderState State { get; set; }

        /// <summary>
        /// Дата создание заказа
        /// </summary>
        [Column("date_create")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата прибытия
        /// </summary>
        [Column("arrive_date")]
        public DateTime ArriveDate { get; set; }

        #region связи

        // Внешний ключ для связи с user
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();


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
