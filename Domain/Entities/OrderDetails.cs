using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectEntityDataBase.Entities
{   
    /// <summary>
     /// Отдельные позиции заказа
     /// </summary>
    [Table("OrderDetails")]
    public class OrderDetails : IDbEntity
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
        /// Количество позиции в заказе
        /// </summary>
        [Required, Column("сount")]
        public int Count { get; set; }

        /// <summary>
        /// Id позиции магазина
        /// </summary>
        [Required, Column("shop_id")]
        public int ShopId { get; set; }
      
       
        #region

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }


        [ForeignKey(nameof(ProductId))]
        public Product product { get; set; }

        [ForeignKey(nameof(ShopId))]
        public Shop shop { get; set; }
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
