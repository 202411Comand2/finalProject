using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Comment
{
    public class AddCommentDto
    {

        /// <summary>
        /// id пользователя
        /// </summary>
        public int UserId { get; set; }
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

      
        public AddCommentDto(int userId, int shopId, int productId, string textComment, decimal estimation)
        {
            UserId = userId;
            ShopId = shopId;
            ProductId = productId;
            TextComment = textComment;
            Estimation = estimation;
        }
    }
}
