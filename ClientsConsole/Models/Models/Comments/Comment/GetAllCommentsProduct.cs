namespace Client.Models
{
    public class GetAllCommentsProduct
    {
        public int IdProduct { get; set; }

        public GetAllCommentsProduct() 
        {
        
        }

        public GetAllCommentsProduct(int idProduct) 
        {
           IdProduct = idProduct;
        }
    }
}
