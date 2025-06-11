namespace CommentService.BLL
{
    public class UpdateCommentDto
    {
       public int CommentId { get; set; }
        public string TextComment { get; set; }
        
        public decimal Estimation { get; set; }

    }
}
