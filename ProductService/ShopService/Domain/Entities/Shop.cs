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


        #region связи на таблицы (id таблиц)
        public ICollection<int> Products { get; set; } = new List<int>();
        #endregion
    }
}
