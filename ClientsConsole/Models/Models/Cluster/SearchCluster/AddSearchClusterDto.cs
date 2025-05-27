using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cluster.SearchCluster
{
    public class AddSearchClusterDto
    {
        /// <summary>
        /// Ключевое слово
        /// </summary>
        public List<string?> KeyWords { get; set; }
        /// <summary>
        /// Id класстера
        /// </summary>
        public int IdCluster { get; set; }
    }
  
}
