using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.DAL;

namespace AuthService.Domain.Entities
{
    /// <summary>
    /// Комментарии пользователей
    /// </summary>
    [Table("Users")]
    public class User : IDbEntity
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required, MaxLength(100), Column("login")]
        public string Login { get; set; } = string.Empty;


        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required, MaxLength(100), Column("name")]
        public string Name { get; set; } = string.Empty;


        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [Required, MaxLength(100), Column("surname")]
        public string Surname { get; set; } = string.Empty;


        /// <summary>
        /// Отчество
        /// </summary>
        [MaxLength(100), Column("patronymic")]
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required, MaxLength(100), Column("password")]
        public string Password { get; set; } = string.Empty;



        /// <summary>
        /// Мобильный телефон пользователя
        /// </summary>
        [Required, MaxLength(12), Column("number_phone")]
        public string NumberPhone { get; set; } = string.Empty;



        /// <summary>
        /// Почта пользователя
        /// </summary>
        [Required, MaxLength(100), Column("email")]
        public string Email { get; set; } = string.Empty;

        
        /// <summary>
        /// Телеграм id 
        /// </summary>
        [Column("telegram_id")]
        public long TelegramID { get; set; }

       

        public int GetPrimaryKey()
        {
            return Id;
        }

    }
}
