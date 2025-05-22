namespace BLL.Dto.Cart
{
    public class CartDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public decimal Count { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
