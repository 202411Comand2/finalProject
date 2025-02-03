using Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Table("Clusters")]
    public class Cluster : IDbEntity
    {
        /// <summary>
        /// Id классификатора
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Название категории классификатора
        /// </summary>
        [Required, MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Id родителя классификатора (о - является корневым)
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Вернуть Id объекта
        /// </summary>
        /// <returns>Возвращает id объекта из бд</returns>
        public int GetPrimaryKey()
        {
            return Id;
        }

        #region
        /// <summary>
        /// Ссылка на родителя
        /// </summary>
        //[ForeignKey(nameof(ParentId))]
        //public Cluster Parent { get; set; }

        /// <summary>
        /// Коллекция продуктов
        /// </summary>
        public ICollection<Product> Products { get; set; } = new List<Product>();
        #endregion

    }
}
