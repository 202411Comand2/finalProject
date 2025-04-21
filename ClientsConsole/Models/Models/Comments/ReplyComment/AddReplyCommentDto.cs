using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comments.ReplyComment
{
    public class AddReplyCommentDto
    {
        public int CommentUserId { get; set; }

        public string? TextComment { get; set; }

        public AddReplyCommentDto() { }

        public AddReplyCommentDto(int commentUserId, string? textComment)
        {
            CommentUserId = commentUserId;
            TextComment = textComment;
        }
    }
}
