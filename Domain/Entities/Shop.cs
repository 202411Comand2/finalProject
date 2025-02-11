using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    /// <summary>
    /// Магазин
    /// </summary>
    [Table("shop")]
    public class Shop : IDbEntity
    {
        /// <summary>
        /// Id магазина
        /// </summary>
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Название магазина
        /// </summary>
        [Required, Column("name"), MaxLength(60)]
        public string? Name { get; set; }

        /// <summary>
        /// Магазин удалён
        /// </summary>
        [Required, Column("is_delete")]
        public bool IsDelete { get; set; } = false;

        /// <summary>
        /// Вернуть Id объекта
        /// </summary>
        /// <returns>Возвращает id объекта из бд</returns>
        public int GetPrimaryKey()
        {
            return Id;
        }


        #region связи
        // Внешний ключ для связи с shopOwner
        //public int ShopOwerId { get; set; }

        //[ForeignKey(nameof(ShopOwerId))]
        //public ShopOwner shopOwner { get; set; }
      
        /// <summary>
        /// Коллекция товара магазина
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();

        ///// <summary>
        ///// Коллекция отзывов о на магазин 
        ///// </summary>
        //public ICollection<Comment> comments { get; set; } = new List<Comment>();
        #endregion
    }
}
