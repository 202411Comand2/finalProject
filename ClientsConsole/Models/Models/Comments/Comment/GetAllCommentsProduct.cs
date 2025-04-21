using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comments.Comment
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
