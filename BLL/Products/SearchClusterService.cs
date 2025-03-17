using BLL.Dto.Cluster;
using BLL.Dto.SearchCluster;
using BLL.Products.Abstractions;
using DAL.Abstractions;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Products
{
    public class SearchClusterService : ISearchClusterService
    {
        private readonly SearchClusterRepository _clusterRepository;
        public SearchClusterService(IContextManager contextManager) => _clusterRepository = new SearchClusterRepository(contextManager);


        public async Task<bool> AddSearchClusterService(AddSearchClusterDto clusterDto)
        {
            return await _clusterRepository.AddSearchElements(clusterDto.IdCluster, clusterDto.KeyWords);
        }
    }
}
