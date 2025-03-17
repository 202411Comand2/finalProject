using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.SearchCluster
{
    public class AddSearchClusterDto
    {
        /// <summary>
        /// Ключевое слово
        /// </summary>
        public string? KeyWord { get; set; }
        /// <summary>
        /// Id класстера
        /// </summary>
        public int IdCluster { get; set; }
    }
  
}
