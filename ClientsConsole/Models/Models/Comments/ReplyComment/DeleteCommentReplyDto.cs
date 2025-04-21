using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comments.ReplyComment
{
    public class DeleteCommentReplyDto
    {
        public int Id { get; set; }

        public DeleteCommentReplyDto() { }

        public DeleteCommentReplyDto(int id)
        { Id = id; }
    }
}
