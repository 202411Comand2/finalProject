namespace Client.Models
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
