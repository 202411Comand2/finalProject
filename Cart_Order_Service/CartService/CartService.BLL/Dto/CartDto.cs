namespace CartService.BLL.Dto
{
    public class CartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; } 
        public decimal Discount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
