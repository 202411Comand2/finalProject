using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BLL.Dto.Product;
using Domain.Entities;

namespace BLL.Dto.SearchCluster
{
    public class SearchClusterProductDto
    {
        public List<Domain.Entities.Cluster> Clusters { get; set; }//= new List<Domain.Entities.Cluster>();
        public List<ProductDto> Products { get;set; } //= new List<Product.ProductDto>();

        public void  SearchClusterProductConnect(List<Domain.Entities.Cluster> clusters,
            List<ProductDto> products) 
        {
             Clusters = clusters;
             Products = products;
        }
    }
}
