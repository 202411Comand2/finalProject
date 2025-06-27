using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.ReplyComment
{
    public class UpdateCommentReplyDto
    {
        public int IdCommentReply { get; set; }
        public string? TextComment { get; set; }  
    }
}
