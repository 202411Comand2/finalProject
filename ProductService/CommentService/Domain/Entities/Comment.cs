using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Platform.DAL;

namespace CommentService.Domain
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
        public int ProductId { get; set; }
       
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
        [ForeignKey(nameof(IdReply))]
        public CommentReply Replies { get; set; }

    }
}
