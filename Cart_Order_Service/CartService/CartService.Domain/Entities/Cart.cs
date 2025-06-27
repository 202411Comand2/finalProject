using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Platform.DAL;

namespace CartService.Domain.Entities
{
    /// <summary>
    /// Корзина пользователя
    /// </summary>

    [Table("Cart")]
    public class Cart : IDbEntity
    {
        /// <summary>
        /// Id позиции в корзине
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        [Column("count")]
        public int Count { get; set; } = 1;

        /// <summary>
        /// Цена товара
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; } = 1;

        /// <summary>
        /// Скидка
        /// </summary>
        [Column("discount")]
        public decimal Discount { get; set; } = 1;

        /// <summary>
        /// Дата добавления товара в корзину
        /// </summary>
        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        #region связи

        public int UserId { get; set; }
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
