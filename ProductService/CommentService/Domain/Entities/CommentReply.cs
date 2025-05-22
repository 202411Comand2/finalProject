using Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    [Table("CommentReplies")]
    public class CommentReply : IDbEntity
    {
        /// <summary>
        /// Id ответа на комментарий
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        /// <summary>
        /// Текстовое описание
        /// </summary>
        [Required, MaxLength(400),Column("text")]
        public string Text { get; set; } = string.Empty;
        
        /// <summary>
        /// Удалён ли комментарий
        /// </summary>
        [Column("is_deleted")]      
        public bool IsDeleted { get; set; } = false;


        /// <summary>
        /// Вернуть Id объекта
        /// </summary>
        /// <returns>Возвращает id объекта из бд</returns>
        public int GetPrimaryKey()
        {
            return Id;
        }

        [Column("id_comment")]
        public int IdComment { get; set; }

        //[ForeignKey(nameof(IdComment))]
        public Comment comment { get; set; }

    }
}
