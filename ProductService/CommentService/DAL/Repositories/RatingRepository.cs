using CommentService.Domain;

namespace CommentService.DAL
{
    public class RatingRepository : BaseRepository<Rating>
    {
        public RatingRepository(IContextManager manager) : base(manager)
        {

        }
    }
}
