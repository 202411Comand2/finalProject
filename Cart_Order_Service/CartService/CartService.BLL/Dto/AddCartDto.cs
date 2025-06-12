namespace CartService.BLL.Dto
{
    public class AddCartDto
    {
        public int UserId { get; set; }        
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
