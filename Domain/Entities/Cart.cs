using Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Корзина пользователя
    /// </summary>

    [Table("Carts")]
    public class Cart : IDbEntity
    {
        /// <summary>
        /// Id позиции в корзине
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id позиции товара
        /// </summary>
        [Required, Column("product_id")]
        public int ProductId { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        [Column("count")]
        public decimal Count { get; set; } = 1;

        /// <summary>
        /// Дата добавления товара в корзину
        /// </summary>
        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;


        #region связи

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product product { get; set; }

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
