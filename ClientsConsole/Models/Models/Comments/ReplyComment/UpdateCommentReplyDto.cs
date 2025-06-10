namespace Client.Models
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
