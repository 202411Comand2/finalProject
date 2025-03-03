using BLL.Dto.Cluster;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Adapters
{
    public class ClusterAdapter
    {
        /// <summary>
        /// Преобразовать из Entitie в Dto
        /// </summary>
        /// <param name="Entitie">Магазин Entitie</param>
        /// <returns>CommentDto</returns>
        public static ClusterDto ConvertFromEntitieToDTO(Cluster Entitie)
        {
            return new ClusterDto()
            {
                Id = Entitie.Id,
                Name = Entitie.Name,
                ParentId = Entitie.ParentId,
            };
        }

        /// <summary>
        /// Преобразовать из Dto в Entitie 
        /// </summary>
        /// <param name="shopDto">Магазин Entitie</param>
        /// <returns>Comment</returns>
        public static Cluster ConvertFromDTOToEntity(ClusterDto Dto)
        {
            return new Cluster
            {
                Id = Dto.Id,
                Name = Dto.Name,
                ParentId = Dto.ParentId,
            };
        }
    }
}
