namespace Client.Models
{
    public class DeleteClusterDto
    {
        public int Id { get; set; }

        public DeleteClusterDto() { }

        public DeleteClusterDto(int id)
        {
            Id = id;
        }
    }
}
