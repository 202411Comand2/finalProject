using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Comment
{
    public class UpdateCommentDto
    {
       public int CommentId { get; set; }
        public string TextComment { get; set; }
        
        public decimal Estimation { get; set; }

    }
}
