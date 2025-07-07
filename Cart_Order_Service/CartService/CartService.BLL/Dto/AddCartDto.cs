using System.ComponentModel.DataAnnotations.Schema;

namespace CartService.BLL.Dto
{
    public class AddCartDto
    {
        public int UserId { get; set; }     
        public int ProductId { get; set; }
        public decimal Price { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public int Count { get; set; } = 1;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
