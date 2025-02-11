using Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Комментарии пользователей
    /// </summary>
    [Table("Comments")]
    public class Comment : IDbEntity
    {
        /// <summary>
        /// id комментариев
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// Оценка потребителя
        /// </summary>
        [Column("estimation")]
        public decimal Estimation { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        [Required, MaxLength(400), Column("Text")]
        public string Text { get; set; }

        /// <summary>
        /// Дата создания комментария
        /// </summary>
        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Содержит удалён ли комментарий?
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// Id ответы
        /// </summary>
        [Column("reply_id")]
        public int IdReply { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required, Column("user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// Id ответы
        /// </summary>
        [Column("shop_id")]
        public int ShopId { get; set; }

        /// <summary>
        /// Id продукта
        /// </summary>
        [Column("id_product")]
        public int IdProduct { get; set; }
       
        /// <summary>
        /// Id ответы
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// Вернуть Id объекта
        /// </summary>
        /// <returns>Возвращает id объекта из бд</returns>
        public int GetPrimaryKey()
        {
            return Id;
        }
        
        #region связи

        [ForeignKey(nameof(IdProduct))]
        public Product Product { get; set; }

        [ForeignKey(nameof(IdReply))]
        public CommentReply Replies { get; set; }
      
        [ForeignKey(nameof(ShopId))]
        public Shop Shop { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        #endregion


        /*
         Comment.IdReply' and 'Comment.UserName' are both mapped to column 'reply_id' in 'Comments', 
            but the properties are contained within the same hierarchy. All properties on an entity type 
            must be mapped to unique different columns."

         */
    }
}
