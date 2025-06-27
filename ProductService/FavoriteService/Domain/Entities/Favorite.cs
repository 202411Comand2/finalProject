using Platform.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FavoriteService.Domain
{
    [Table("Favorite")]
    public class Favorite : IDbEntity
    {
        /// <summary>
        /// Id избранной позиции
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
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

        public int User { get; set; }

        public int Product { get; set; }

    
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
