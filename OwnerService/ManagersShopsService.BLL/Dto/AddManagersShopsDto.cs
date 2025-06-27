using Platform.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagersShopsService.BLL.Dto
{
    public class AddManagersShopsDto
    {
        /// <summary>
        /// id пользователя магазина
        /// </summary>
        public int UserId { get; set; }


        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string RoleUser { get; set; } = string.Empty;

        /// <summary>
        /// Название магазина
        /// </summary>
        public string NameShop { get; set; } = string.Empty;


        /// <summary>
        /// id созданного магазина
        /// </summary>
        public int ShopId { get; set; } 
    }

}
