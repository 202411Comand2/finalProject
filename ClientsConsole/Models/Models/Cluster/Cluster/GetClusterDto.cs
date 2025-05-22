using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cluster.Clusters
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
