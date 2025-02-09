using Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Favorite")]
    public class Favorite : IDbEntity
    {
        /// <summary>
        /// Id избранной позиции
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// id избранного пользователя
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// id избранного пользователя
        /// </summary>
        [Column("id_product")]
        public int IdProduct { get; set; }
      
        
        #region

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(IdProduct))]
        public Product Product { get; set; }


        /// <summary>
        /// Коллекция избранных продуктов пользователем
        /// </summary>
      //  public ICollection<Product> Products { get; set; } = new List<Product>();
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
