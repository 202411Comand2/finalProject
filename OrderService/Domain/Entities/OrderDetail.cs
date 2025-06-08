using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
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
        /// Id заказа, к которому идёт эта позиция
        /// </summary>
        [Required,Column("order_id")]
        public int OrderId { get; set; }

        /// <summary>
        /// Id позиции товара
        /// </summary>
        [Required, Column("product_id")]
        public int ProductId { get; set; }

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
        /// Id позиции магазина
        /// </summary>
        [Required, Column("shop_id")]
        public int ShopId { get; set; }
      
       
        #region

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }


        /*[ForeignKey(nameof(ProductId))]
        public Product product { get; set; }

        [ForeignKey(nameof(ShopId))]
        public Shop shop { get; set; }*/
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
