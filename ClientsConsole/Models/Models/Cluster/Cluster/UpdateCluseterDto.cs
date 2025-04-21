using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cluster.Clusters
{
    public class UpdateCluseterDto
    {

        public int Id { get; set; }

        public string NewName { get; set; }


        public string  newParent { get; set; }

        public UpdateCluseterDto() { }

        public UpdateCluseterDto(int id, string newName, string newParent)
        {
            Id = id;
            NewName = newName;
            this.newParent = newParent;
        }



    }
}
