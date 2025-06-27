using OrderService.Domain.Enums;
using Platform.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Domain.Entities
{   
    /// <summary>
    /// Отдельные позиции заказа
    /// </summary>
    [Table("OrderDetail")]
    public class OrderDetail : IDbEntity
    { 
        /// <summary>
      /// Id заказа
      /// </summary>
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Количество позиций в заказе
        /// </summary>
        [Required, Column("сount")]
        public int Count { get; set; }

        /// <summary>
        /// Цена в заказе
        /// </summary>
        [Required, Column("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Статус позиции в заказе
        /// </summary>
        [Required, Column("StatusPosition")]
        public int StatusPosition { get; set; }

        #region

        public int OrderId { get; set; }
        public int ProductId { get; set; }

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
