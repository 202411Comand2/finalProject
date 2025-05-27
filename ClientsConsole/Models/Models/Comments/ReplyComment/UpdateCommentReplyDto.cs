using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comments.ReplyComment
{
    public class UpdateCommentReplyDto
    {
        public int IdCommentReply { get; set; }
        public string? TextComment { get; set; }  

        public UpdateCommentReplyDto() { }

        public UpdateCommentReplyDto(int idCommentReply, string? textComment)
        {
            IdCommentReply = idCommentReply;
            TextComment = textComment;
        }
    }
}
