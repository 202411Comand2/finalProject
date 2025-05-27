using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comments.Comment
{
    public class DeleteCommentDto
    {
        public int Id { get; set; }

        public DeleteCommentDto() { }

        public DeleteCommentDto(int id) 
        {
            Id = id;
        }

    }
}
