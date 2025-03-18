namespace BLL.Dto.Cart
{
    public class AddCartDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Count { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
