using Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Table("Products")]
    public class Product : IDbEntity
    {
        /// <summary>
        /// Id продукта
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column("id")]
        public int Id { get; set; }


        /// <summary>
        /// Продукт удалён
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Номер модели
        /// </summary>
        [Required, MaxLength(50), Column("model_number")]
        public string ModelNumber { get; set; } = string.Empty;

        /// <summary>
        /// Наименование продукта
        /// </summary>
        [Required, MaxLength(30), Column("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание продукта
        /// </summary>
        [Required, MaxLength(255), Column("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Id рейтинга
        /// </summary>
        //[Column("rating_id")]
        //public int RatingId { get; set; }

        /// <summary>
        /// Id классификатора продукта
        /// </summary>
        [Column("cluster_id")]
        public int ClusterId { get; set; }

        /// <summary>
        /// Цена продукта
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Штрих код
        /// </summary>
        [Column("barcode")]
        public long Barcode { get; set; }


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
            return Id;
        }

        #region связи на таблицы (id таблиц)
        public int ShopId { get; set; } = new int();
        public int ClusterID { get; set; } = new int();
        //public ICollection<int> ClustersID { get; set; } = new List<int>();

        #endregion




    }
}
