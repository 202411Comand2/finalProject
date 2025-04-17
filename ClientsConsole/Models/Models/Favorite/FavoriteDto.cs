using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Favorite
{

    public class FavoriteDto
    {
        /// <summary>
        /// Id объекта
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Id продукта
        /// </summary>
        public int ProductId { get; set; }
    }
}
