using Platform.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BLL.Dto
{
    public class AddUserDto
    {

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; } = string.Empty;


        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; } = string.Empty;


        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; } = string.Empty;

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; } = string.Empty;
        
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Мобильный телефон пользователя
        /// </summary>
        public string NumberPhone { get; set; } = string.Empty;

        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Индефикатор пользователя
        /// </summary>
        public long TelegramID { get; set; }


    }

}
