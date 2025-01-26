using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectEntityDataBase.Entities
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
        public string Text { get; set; }
      
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

        [ForeignKey("IdComment")]
        public Comment comment { get; set; }

    }
}
