using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cluster.SearchCluster
{
    public class DeleteSearchClusterDto
    {
        /// <summary>
        /// ID ключевых слов, которые нужно удалить
        /// </summary>
       public List<int> IdSearchClusterDto {  get; set; }
    }
}
