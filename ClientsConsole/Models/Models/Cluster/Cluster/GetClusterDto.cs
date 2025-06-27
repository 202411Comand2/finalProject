namespace Client.Models
{
    public class GetClusterDto
    {
        //public int ClusterId { get; set; } 

        public string ClusterName { get; set; }
        
        public GetClusterDto() { }

        public GetClusterDto(string clusterName) 
        {
            ClusterName = clusterName ?? string.Empty;
        }
    }
}
