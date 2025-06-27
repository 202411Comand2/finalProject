using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.SearchCluster
{
    public class UpdateSearchClusterDto
    {
        public int SearchClusterId { get; set; }

        public string? SearchClusterKeyWord { get; set; }
    }
}
