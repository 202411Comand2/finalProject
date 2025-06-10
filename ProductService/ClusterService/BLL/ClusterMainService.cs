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


        //public async Task<List<Cluster>> GetChildrenElementsCluster(int clusterId)
        //{
        //    Cluster cluster = await _clusterSearchRepository.Get(clusterId);
        //    if (cluster is null)
        //    {
        //        //если такого кластера нет
        //        return null;
        //    }
        //    else
        //    {
        //        return await _clusterSearchRepository.GeElementsClaster(clusterId);
        //    }
        //}

        //public async Task<List<Cluster>> GetChildrenElementsCluster(string nameCluster)
        //{
        //    Cluster cluster = await _clusterSearchRepository.GetNameCluster(nameCluster);

        //    if (cluster is null)
        //    {//"Ошибка. Не найден кластер по имени"
        //        return null;
        //    }
        //    return await _clusterSearchRepository.GeElementsClaster(cluster.ClusterId);
        //}




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



        //public async Task<bool> UpdateNameCluster(int clasterId, string newNameClaster)
        //{
        //    Cluster cluster = await _clusterSearchRepository.Get(clasterId);
        //    Cluster clusterNewName = await _clusterSearchRepository.GetNameCluster(newNameClaster);

        //    if (cluster is null)
        //    {
        //        return false; //не получилось изменить название классификатора. Не получилось найти указанный кластер в базе
        //    }
        //    else
        //    {
        //        if (clusterNewName is null)
        //        {
        //            cluster.Name = newNameClaster;
        //            await _clusterSearchRepository.Update(cluster);
        //            return true;// "Кластер изменён";
        //        }
        //        else
        //        {
        //            return false;// "не получилось изменить название классификатора. Имя этого кластера занято!";
        //        }
        //    }
        //}

        //public async Task<bool> UpdateNameCluster(string oldNameClaster, string newNameClaster)
        //{
        //    Cluster cluster = await _clusterSearchRepository.GetNameCluster(oldNameClaster);
        //    Cluster clusterNewName = await _clusterSearchRepository.GetNameCluster(newNameClaster);

        //    if (cluster is null)
        //    {
        //        return false;// "не получилось изменить название классификатора. Не получилось найти указанный кластер в базе";
        //    }
        //    else
        //    {

        //        if (clusterNewName is null)
        //        {
        //            cluster.Name = newNameClaster;
        //            await _clusterSearchRepository.Update(cluster);
        //            return true;//"Кластер изменён";
        //        }
        //        else
        //        {
        //            return false;// "не получилось изменить название классификатора. Имя этого кластера занято!";
        //        }
        //    }
        //}

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

        //public async Task<bool> UpdatePositionCluster(int clusterId, int parentId)
        //{
        //    Cluster cluster = await _clusterSearchRepository.Get(clusterId);
        //    if (cluster is null)
        //    {
        //        return false;//Ошибка. Не найден кластер по id
        //    }
        //    else
        //    {
        //        if (parentId == cluster.ParentId)
        //        {
        //            return false;// "Изменения не нужны, так как перемещения не произошло.";
        //        }
        //        if (parentId == -1)
        //        {
        //            cluster.ParentId = -1;
        //            await _clusterSearchRepository.Update(cluster);
        //            return true;// "Изменения были приняты иерархия была изменена";
        //        }

        //        Cluster clusterParent = await _clusterSearchRepository.Get(parentId);
        //        if (clusterParent is not null)
        //        {
        //            cluster.ParentId = parentId;
        //            await _clusterSearchRepository.Update(cluster);
        //            return true; //"Изменения были приняты иерархия была изменена";
        //        }
        //        else
        //        {
        //            return false;// "Ошибка. Родительский кластер не найден!";
        //        }
        //    }
        //}

        //public async Task<bool> UpdatePositionCluster(string nameCluster, int parentId)
        //{
        //    Cluster cluster = await _clusterSearchRepository.GetNameCluster(nameCluster);

        //    if (cluster is null)
        //    {
        //        return false;// "Ошибка. Не найден кластер по id";
        //    }
        //    else
        //    {
        //        if (parentId == cluster.ParentId)
        //        {
        //            return true;// "Изменения не нужны, так как перемещения не произошло.";
        //        }
        //        if (parentId == -1)
        //        {
        //            cluster.ParentId = -1;
        //            await _clusterSearchRepository.Update(cluster);
        //            return true;// "Изменения были приняты иерархия была изменена";
        //        }

        //        Cluster clusterParent = await _clusterSearchRepository.Get(parentId);
        //        if (clusterParent is not null)
        //        {
        //            cluster.ParentId = parentId;
        //            await _clusterSearchRepository.Update(cluster);
        //            return true;// "Изменения были приняты иерархия была изменена";
        //        }
        //        else
        //        {
        //            return false;//"Ошибка. Родительский кластер не найден!";
        //        }
        //    }
        //}

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
    }
}
