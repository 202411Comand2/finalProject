using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cluster.Clusters
{
    public class DeleteClusterDto
    {
        public int Id { get; set; }

        public DeleteClusterDto() { }

        public DeleteClusterDto(int id)
        {
            Id = id;
        }
    }
}
