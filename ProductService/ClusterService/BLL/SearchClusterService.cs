using BLL.Dto.Cluster;
using BLL.Dto.SearchCluster;
using BLL.Products.Abstractions;
using DAL;
using DAL.Abstractions;
using DAL.Repositories;
using Domain.Entities;

namespace BLL.Products
{
    public class SearchClusterService : ISearchClusterService
    {
        private readonly SearchClusterRepository _clusterSearchRepository;

        private readonly ClusterRepository _clusterRepository;

        public SearchClusterService(IContextManager contextManager)
        {
            _clusterSearchRepository = new SearchClusterRepository(contextManager);
            _clusterRepository = new ClusterRepository(contextManager);
        }

        public async Task<bool> AddSearchClusterService(AddSearchClusterDto clusterDto)
        {
            return await _clusterSearchRepository.AddSearchElements(clusterDto.IdCluster, clusterDto.KeyWords);
        }

        public async Task<bool> DeleteSearchClusterService(DeleteSearchClusterDto deleteSearchClusterDto)
        {
            return await _clusterSearchRepository.AddSearchElements(deleteSearchClusterDto.IdSearchClusterDto);
        }

        public async Task<bool> UpdateSearchClusterService(UpdateSearchClusterDto updateSearchClusterDto)
        {
            if (updateSearchClusterDto == null || string.IsNullOrEmpty(updateSearchClusterDto.SearchClusterKeyWord))
            {
                return false;
            }
            SearchCluster searchClusterRepository =
                await _clusterSearchRepository.Get(updateSearchClusterDto.SearchClusterId);

            if (searchClusterRepository != null)
            {
                searchClusterRepository.KeyWord = updateSearchClusterDto.SearchClusterKeyWord;
                await _clusterSearchRepository.Update(searchClusterRepository);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<GetSearchClusterDto> GetSearchClusterId(int id)
        {
            if (await _clusterSearchRepository.CheckClusterForKeywords(id))
            {
                List<SearchCluster> info = await _clusterSearchRepository.GetSearchClusters(id);
                GetSearchClusterDto getSearchClusterDto = new GetSearchClusterDto();
                getSearchClusterDto.ClusterId = id;

                foreach (SearchCluster searchCluster in info)
                {
                    getSearchClusterDto.InfoPosition.Add(new GetSearchClusterDto.Info(searchCluster.Id, searchCluster.KeyWord));
                }
                return getSearchClusterDto;

            }
            return null;
        }

        public async Task<(List<Cluster>, List<int>)> SearchProducts(string keyWord)
        {
            List<int> s = await _clusterSearchRepository.СompleteМatch(keyWord);
            List<Cluster> clusterCollection= await _clusterRepository.GetArrayCluster(s);
            List<int> clusterId = new List<int>();
            foreach (Cluster cluster in clusterCollection) 
            {
                clusterId.Add(cluster.Id);
            }
            return (clusterCollection, clusterId);
        }
    }
}
