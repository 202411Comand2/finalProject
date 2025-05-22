using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.ReplyComment
{
    public class AddReplyCommentDto
    {
       public int CommentUserId { get; set; }

       public string? TextComment { get; set; }
    }
}
