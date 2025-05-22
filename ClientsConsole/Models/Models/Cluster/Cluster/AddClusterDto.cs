using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cluster.Clusters
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

        public AddClusterDto() { }

        public AddClusterDto(string name, string nameParentCluseter)
        {
            Name = name;
            NameParentCluseter = nameParentCluseter;
        }
    }
}
