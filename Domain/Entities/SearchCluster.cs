using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("search_cluster")]
    public class SearchCluster
    {
        /// <summary>
        /// Id классификатора
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Ключевое слово кластера по которому нужно делать поиск
        /// </summary>
        [Required]
        public string KeyWord { get; set; } = string.Empty;

        /// <summary>
        /// Id кластера
        /// </summary>
        [Required]
        public int ClusterId { get; set; }

        /// <summary>
        /// Вернуть Id объекта
        /// </summary>
        /// <returns>Возвращает id объекта из бд</returns>
        public int GetPrimaryKey()
        {
            return Id;
        }
    }
}
