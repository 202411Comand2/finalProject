using DAL.Abstractions;
using FinalProjectEntityDataBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    
    public class CommentReplyRepository : BaseRepository<CommentReply>
    {
        public CommentReplyRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
