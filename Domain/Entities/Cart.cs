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
        /// Вернуть Id объекта
        /// </summary>
        /// <returns>Возвращает id объекта в бд</returns>
        public int GetPrimaryKey()
        {
            return Id;
        }

        #region
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }


        public ICollection<CartItem> cartItems { get; set; } = new List<CartItem>();


        #endregion
    }
}
