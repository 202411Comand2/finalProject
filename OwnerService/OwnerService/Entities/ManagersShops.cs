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
    [Table("ManagersShops")]
    public class Comment : IDbEntity
    {
        /// <summary>
        /// Id магазина
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// id магазина
        /// </summary>
        [Required, Column("shop_id")]
        public int ShopID { get; set; }


        /// <summary>
        /// id пользователя магазина
        /// </summary>
        [Required, Column("user_id")]
        public int UserId { get; set; }



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
