using ClusterService.Domain;

namespace ClusterService.BLL
{
    public class SearchClusterProductDto
    {
        public List<Cluster> Clusters { get; set; }//= new List<Domain.Entities.Cluster>();
        public List<int> Products { get;set; } //= new List<Product.ProductDto>();

        public void  SearchClusterProductConnect(List<Cluster> clusters,
            List<int> products) 
        {
             Clusters = clusters;
             Products = products;
        }
    }
}
