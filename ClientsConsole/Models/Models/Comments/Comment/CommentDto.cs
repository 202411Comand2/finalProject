using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comments.Comment
{
    public class CommentDto
    {     
        /// <summary>
        /// Id
        /// </summary>
        public int Id;
        
        /// <summary>
             /// id пользователя
             /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// НикНейм пользователя
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// id магазина
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// id продукта
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// id пользователя
        /// </summary>
        public string TextComment { get; set; } = string.Empty;
        /// <summary>
        /// Оценка выставляемая пользователем за товар
        /// </summary>
        public decimal Estimation { get; set; }
    }
}
