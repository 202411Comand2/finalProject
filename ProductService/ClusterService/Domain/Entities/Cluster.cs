using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Platform.DAL;

namespace ClusterService.Domain
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

        // Навигационное свойство для связи
        public ICollection<Link> Links { get; set; } = new List<Link>();

    }
}


