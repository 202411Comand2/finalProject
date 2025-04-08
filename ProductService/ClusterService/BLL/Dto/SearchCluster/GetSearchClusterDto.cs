using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.SearchCluster
{
    public class GetSearchClusterDto
    {
        public  int ClusterId { get; set; }
         
        public List<Info> InfoPosition { get; set; } = new List<Info>();


        public class Info 
        {
            public int SearchClusterId { get; set; }

            public string? SearchClusterKeyWord { get; set; }

            public Info(int searchClusterId, string? searchClusterKeyWord)
            {
                SearchClusterId = searchClusterId;
                SearchClusterKeyWord = searchClusterKeyWord;
            }
        } 
    }
}
