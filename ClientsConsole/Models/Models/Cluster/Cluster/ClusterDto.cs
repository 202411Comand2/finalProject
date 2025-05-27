using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cluster.Clusters
{
    public class ClusterDto
    {
        /// <summary>
        /// ClusterId классификатора
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название категории классификатора
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// ClusterId родителя классификатора (о - является корневым)
        /// </summary>
        public int ParentId { get; set; }

    }
}
