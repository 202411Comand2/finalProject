namespace Client.Models
{
    public class UpdateCommentDto
    {
        public int CommentId { get; set; }
        public string TextComment { get; set; } = string.Empty;
        public decimal Estimation { get; set; }


        public UpdateCommentDto() { }

        public UpdateCommentDto(int commentId, string text, decimal estimation) 
        {
            CommentId = commentId;
            TextComment = text;
            Estimation = estimation;
        }
    }
}
