using Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Средний рейтинг товара
    /// </summary>
    [Table("Ratings")]
    public class Rating : IDbEntity
    {
        /// <summary>
        /// Id рейтинга
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RatingId { get; set; }

        /// <summary>
        /// Средний рейтинг оценки продукта
        /// </summary>
        [Column("average_rating")]
        public decimal AverageRating { get; set; }

        /// <summary>
        /// Количество комментариев
        /// </summary>
        [Column("amount_of_comments")]
        public int AmountOfComments { get; set; }
      
        /// <summary>
        /// Вернуть Id объекта
        /// </summary>
        /// <returns>Возвращает id объекта из бд</returns>
        public int GetPrimaryKey()
        {
            return RatingId;
        }

        #region связи
        /// <summary>
        /// Id продукта
        /// </summary>
        public int ProductID { get; set; }

        //[ForeignKey(nameof(ProductID))]
        //public Product Product { get; set; }

        #endregion
    }
}
