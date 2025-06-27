using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.DAL;

namespace OwnerService.Domain.Entities
{
    /// <summary>
    /// Комментарии пользователей
    /// </summary>
    [Table("ManagersShops")]
    public class ManagersShops : IDbEntity
    {
        /// <summary>
        /// Id записи
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// id магазина
        /// </summary>
        [Required, Column("shop_id")]
        public int ShopId { get; set; }


        /// <summary>
        /// id пользователя магазина
        /// </summary>
        [Required, Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Название магазина
        /// </summary>
        [Required, Column("name_shop")]
        public string NameShop { get; set; } = string.Empty;


        /// <summary>
        /// Роль пользователя
        /// </summary>
        [Required, MaxLength(100), Column("user_name")]
        public string UserName { get; set; } = string.Empty;


        /// <summary>
        /// Роль пользователя
        /// </summary>
        [Required, MaxLength(100), Column("role_user")]
        public string RoleUser { get; set; } = string.Empty;

        public int GetPrimaryKey()
        {
            return Id;
        }

    }
}
