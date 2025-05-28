using BLL.Dto.Product;

namespace BLL.Dto.SearchCluster
{
    public class SearchClusterProductDto
    {
        public List<Domain.Entities.Cluster> Clusters { get; set; }
        public List<ProductDto> Products { get;set; }
        public void  SearchClusterProductConnect(List<Domain.Entities.Cluster> clusters,
            List<ProductDto> products) 
        {
             Clusters = clusters;
             Products = products;
        }
    }
}
