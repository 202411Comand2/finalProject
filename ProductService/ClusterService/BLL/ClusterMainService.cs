using ClusterService.DAL;
using ClusterService.Domain;
using SupperBackEnd.Dto;

namespace ClusterService.BLL
{
    public class ClusterMainService : IClusterMainService
    {
        private readonly ClusterRepository _clusterRepository;
        public ClusterMainService(IContextManager contextManager) => _clusterRepository = new ClusterRepository(contextManager);


        public async Task<AnswerWithBackendDto<ClusterDto>> AddNewCluster(AddClusterDto clusterDto)
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            Cluster cluster = await _clusterRepository.GetNameCluster(clusterDto.Name);
            Cluster clusterParent = await _clusterRepository.GetNameCluster(clusterDto.NameParentCluseter);

            if (cluster is not null)
            {
                result.AddErrorLog("Не получилось создать кластер, так как он уже существует.");
                return result;
            }
            if (cluster is null && string.IsNullOrEmpty(clusterDto.NameParentCluseter))
            {
                //если родитель явно не указан, т.е. является корнем
                cluster = new Cluster
                {
                    Name = clusterDto.Name,
                    ParentId = -1,
                };
                // var result = await _clusterSearchRepository.Add(cluster);
                var item = await _clusterRepository.Add(cluster);
                if (item is not null)
                {
                    result.AddObject(ClusterAdapter.ConvertFromEntitieToDTO(item));
                    return result;
                }
                else
                {
                    result.AddErrorLog("Не получилось создать кластер");
                    return result;
                }

            }

            if (clusterParent is null)
            {
                result.AddErrorLog("Не получилось создать кластер, так как указанный родителей не был найден в бд");
                return result;
            }
            if (cluster is null)
            {
                cluster = new Cluster
                {
                    Name = clusterDto.Name,
                    ParentId = clusterParent.Id
                };
                // var result = await _clusterSearchRepository.Add(cluster);
                var item = await _clusterRepository.Add(cluster);
                if (item is not null)
                {
                    result.AddObject(ClusterAdapter.ConvertFromEntitieToDTO(item));
                    return result;
                }
                else
                {
                    result.AddErrorLog("Не получилось создать кластер");
                    return result;
                }
            }
            else
            {
                result.AddErrorLog("Не получилось создать кластер. Так какой уже существует в бд.");
                return result; ;//Не удалось создать в виду наличия в системе уже существующего кластера
            }
        }
        public async Task<AnswerWithBackendDto<ClusterDto>> DeleteCluster(DeleteClusterDto deleteClusterDto)
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            Cluster cluster = await _clusterRepository.Get(deleteClusterDto.Id);
            if (cluster is null)
            {
                result.AddErrorLog("Не удалось удалить кластер ввиду его отсутствия в бд!");
                return result; // Не получилось удалить ввиду отсутствия id кластера
            }
            else
            {
                if (await _clusterRepository.Delete(cluster))
                {
                    result.DataReceived = true;
                }
                else
                {
                    result.AddErrorLog("Не получилось удалить объект. Проблемы с бд.");
                }
                return result; // Кластер удалён

            }
        }
        public async Task<AnswerWithBackendDto<ClusterDto>> GetChildrenElementsCluster(GetClusterDto getClusterDto)
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            Cluster cluster = await _clusterRepository.GetNameCluster(getClusterDto.ClusterName);
            if (cluster is null)
            {//"Ошибка. Не найден кластер по имени"
                result.AddErrorLog("Ошибка. Не найден кластер по id");
                return result;
            }
            var items = ClusterAdapter.ConvertFromEntitieToDTO((List<Cluster>)await _clusterRepository.GeElementsCluster(cluster.Id));
            if (items is null)
            {
                result.AddErrorLog("У указанно кластера отсутствуют дочерние объекты");
            }
            else 
            { 
            result.AddObject(items);

            }
            return result;
        }
        public async Task<AnswerWithBackendDto<ClusterDto>> UpdateNameCluster(UpdateCluseterDto updateCluseterDto)
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            Cluster cluster = await _clusterRepository.Get(updateCluseterDto.Id);
            Cluster clusterNewName = await _clusterRepository.GetNameCluster(updateCluseterDto.NewName);
            Cluster clusterParent = await _clusterRepository.GetNameCluster(updateCluseterDto.newParent);
            if (cluster is null)
            {
                result.AddErrorLog("не получилось изменить название классификатора. Не получилось найти указанный кластер в базе");
                return result;
            }
            else
            {

                if (cluster is not null && string.IsNullOrEmpty(updateCluseterDto.newParent) && clusterNewName is null)
                {
                    cluster.Name = updateCluseterDto.NewName;
                    cluster.ParentId = -1;
                    var itemUpdate = await _clusterRepository.Update(cluster);
                    if (itemUpdate is not null)
                    {
                        result.AddObject(ClusterAdapter.ConvertFromEntitieToDTO(itemUpdate));
                        return result;
                    }
                    else
                    {
                        result.AddErrorLog("Ошибка бд.");
                        return result;
                    }
                }

                if (cluster is not null && clusterParent is not null && clusterNewName is null)
                {
                    cluster.Name = updateCluseterDto.NewName;
                    cluster.ParentId = clusterParent.Id;
                    var itemUpdate = await _clusterRepository.Update(cluster);
                    if (itemUpdate is not null)
                    {
                        result.AddObject(ClusterAdapter.ConvertFromEntitieToDTO(itemUpdate));
                        return result;
                    }
                    else
                    {
                        result.AddErrorLog("Ошибка бд.");
                        return result;
                    }

                }
                result.AddErrorLog("не получилось изменить название классификатора. Имя этого кластера занято!");
                return result;
            }
        }
        public async Task<AnswerWithBackendDto<ClusterDto>> GetAllElementsCluster()
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            List<ClusterDto> clusters = new List<ClusterDto>();
            result.AddObject(ClusterAdapter.ConvertFromEntitieToDTO( (List<Cluster>)await _clusterRepository.GetAll()));
            return result;
        }
        public async Task<AnswerWithBackendDto<ClusterDto>> GetRootElementsCluster()
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            result.AddObject(ClusterAdapter.ConvertFromEntitieToDTO((List<Cluster>)await _clusterRepository.GetRootElementsClaster()));
            return result;
        }

        public async Task<AnswerWithBackendDto<ClusterDto>> GetChildrenElementsCluster(int id)
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            //List <Cluster> cluster = await _clusterRepository.GetChildClusterIdsOptimized(id);
            //if (cluster is null)
            //{//"Ошибка. Не найден кластер по имени"
            //    result.AddErrorLog("Ошибка. Не найден кластер по id");
            //    return result;
            //}
          
            var items = ClusterAdapter.ConvertFromEntitieToDTO((List<Cluster>)await _clusterRepository.GetChildClusterIdsOptimized(id));
            if (items is null)
            {
                result.AddErrorLog("У указанно кластера отсутствуют дочерние объекты");
            }
            else
            {
                result.AddObject(items);

            }
            return result;
        }

        public async Task<AnswerWithBackendDto<ClusterDto>> SerchCluster(string keyWord)
        {
            AnswerWithBackendDto<ClusterDto> result = new();
            //List<Cluster> cluster = await _clusterRepository.GetSearchCluster(keyWord);
            //if (cluster is null)
            //{//"Ошибка. Не найден кластер по имени"
            //    result.AddErrorLog("Ошибка. Не найден кластер по id");
            //    return result;
            //}

            var items = ClusterAdapter.ConvertFromEntitieToDTO((List<Cluster>)await _clusterRepository.GetSearchCluster(keyWord));
            if (items is null)
            {
                result.AddErrorLog("У указанно кластера отсутствуют дочерние объекты");
            }
            else
            {
                result.AddObject(items);

            }
            return result;
        }
    }
}
