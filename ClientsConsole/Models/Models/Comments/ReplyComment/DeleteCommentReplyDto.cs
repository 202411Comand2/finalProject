namespace Client.Models
{
    public class DeleteCommentReplyDto
    {
        public int Id { get; set; }

        public DeleteCommentReplyDto() { }

        public DeleteCommentReplyDto(int id)
        { Id = id; }
    }
}
