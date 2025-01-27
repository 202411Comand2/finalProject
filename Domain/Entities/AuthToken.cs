using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectEntityDataBase.Entities
{
    /// <summary>
    /// Аутификация
    /// </summary>
    [Table("auth_token")]
    public class AuthToken : IDbEntity
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key, Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// Guid аутификации для сессии пользователя
        /// </summary>
        [Required, Column("token_id")]
        public Guid TokenId { get; set; }


        /// <summary>
        /// Название устройства 
        /// </summary>
        [Required, Column("device"), MaxLength(250)]
        public string? Device { get; set; }

        /// <summary>
        /// Дата создания токена авторизации 
        /// </summary>
        [Required, Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата истечения срока действия токена 
        /// </summary>
        [Required, Column("expire_date")]
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// Вернуть Id объекта
        /// </summary>
        /// <returns>Возвращает id объекта из бд</returns>
        public int GetPrimaryKey()
        {
            return Id;
        }

        #region связи
        /// <summary>
        /// Связь много(токенов) к одну (пользователю)
        /// </summary>
        public ICollection<User> Users { get; set; } = new List<User>();
        
        #endregion
    }
}
