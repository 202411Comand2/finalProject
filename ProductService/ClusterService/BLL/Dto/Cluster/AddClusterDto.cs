using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto.Cluster
{
    /// <summary>
    /// Добавить новый кластер
    /// </summary>
    public class AddClusterDto
    {
        /// <summary>
        /// Имя кластера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Родительский кластер
        /// </summary>
        public string NameParentCluseter { get; set; }
    }
}
