using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Favorite
{
    public class AddFavoriteDto
    {
       public int UserId { get; set; }
        
       public int ProductId { get; set; }
    }
}
