namespace Client.Models
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
