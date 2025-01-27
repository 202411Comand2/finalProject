using Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// 1 позиция товара в корзине
    /// </summary>
    public class CartItem : IDbEntity
    {

        /// <summary>
        /// Id 
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        [Column("count")]
        public decimal Count { get; set; } = 1;
       
        
        #region связи
        public int ProductID { get; set; }

        [ForeignKey(nameof(ProductID))]
        public Product Product { get; set; }
       


        public int CartId { get; set; }

        public Cart Cart { get; set; } 

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
