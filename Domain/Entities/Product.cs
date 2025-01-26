using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectEntityDataBase.Entities
{
    [Table("Products")]
    public class Product : IDbEntity
    {
        /// <summary>
        /// Id продукта
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Номер модели
        /// </summary>
        [Required, MaxLength(50), Column("model_number")]
        public string ModelNumber { get; set; }

        /// <summary>
        /// Наименование продукта
        /// </summary>
        [Required, MaxLength(30), Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Описание продукта
        /// </summary>
        [Required, MaxLength(255), Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// Id рейтинга
        /// </summary>
        [Column("rating_id")]
        public int RatingId { get; set; }

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
        /// Вернуть Id объекта
        /// </summary>
        /// <returns>Возвращает id объекта из бд</returns>
        public int GetPrimaryKey()
        {
            return Id;
        }

        #region связи
        // Внешний ключ для связи с shopOwner
        public int ShopId { get; set; }

        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }


        // Внешний ключ для связи с ClusterId
        ///   public int ClusterId { get; set; }

        [ForeignKey("ClusterId")]
        public Cluster Cluster { get; set; }

        [ForeignKey("RatingId")]
        public Rating Rating { get; set; }
        #endregion




    }
}
