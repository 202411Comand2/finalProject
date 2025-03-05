using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DAL.Repositories;
using System.Xml.Linq;
using DAL.Abstractions;
using BLL.Products.Abstractions;
using BLL.Dto.Cluster;
using System.Diagnostics.Metrics;
using System.Formats.Asn1;
using static Pipelines.Sockets.Unofficial.SocketConnection;


namespace BLL.Products
{
    public class ClusterService : IClusterService
    {
        private readonly ClusterRepository _clusterRepository;
        public ClusterService(IContextManager contextManager) => _clusterRepository = new ClusterRepository(contextManager);

        //public async Task<bool> AddNewCluster(ClusterDto clusterDto)
        //{
        //    Cluster cluster = await _clusterRepository.GetNameCluster(clusterDto.Name);
        //    if (cluster is null)
        //    {
        //        if (clusterDto.ParentId == -1)
        //        {
        //            cluster = new Cluster
        //            {
        //                Name = clusterDto.Name,
        //                ParentId = -1,
        //            };
        //            var result = await _clusterRepository.Add(cluster);
        //            return true; //Кластер создан
        //        }
        //        else
        //        {
        //            Cluster clusterParent = await _clusterRepository.Get(clusterDto.ParentId);
        //            if (clusterParent is null)
        //            {
        //                return false; // По указанному id не нашёл родителя
        //            }
        //            cluster = new Cluster
        //            {
        //                Name = clusterDto.Name,
        //                ParentId = clusterDto.ParentId,
        //            };
        //            var result = await _clusterRepository.Add(cluster);
        //            return true;// "Кластер создан";
        //        }
        //    }
        //    else
        //    {
        //        return false;   //Не удалось создать в виду наличия в системе уже существующего кластера
        //    }
        //}

        //public async Task<bool> AddNewCluster(ClusterDto clusterDto)
        //{
        //    Cluster cluster = await _clusterRepository.GetNameCluster(clusterDto.Name);
        //    Cluster clusterParent = await _clusterRepository.Get(clusterDto.ParentId);
        //    if (clusterParent is null)
        //    {
        //        return false; // По указанному id не нашёл родителя
        //    }
        //    if (cluster is null)
        //    {
        //        cluster = new Cluster
        //        {
        //            Name = clusterDto.Name,
        //            ParentId = clusterDto.ParentId,
        //        };
        //        var result = await _clusterRepository.Add(cluster);
        //        return true;// "Кластер создан";
        //    }
        //    else
        //    {
        //        return false;//Не удалось создать в виду наличия в системе уже существующего кластера
        //    }
        //}

        //public async Task<bool> AddNewCluster(string nameClaster, string NameParent)
        //{
        //    Cluster cluster = await _clusterRepository.GetNameCluster(nameClaster);
        //    Cluster clusterParent = await _clusterRepository.GetNameCluster(NameParent);

           
        //    if (clusterParent is null)
        //    {
        //        return false;//"По указанному имени не нашёл родителя";
        //    }
        //    if (cluster is null)
        //    {
        //        cluster = new Cluster
        //        {
        //            Name = nameClaster,
        //            ParentId = clusterParent.ClusterId
        //        };
        //        var result = await _clusterRepository.Add(cluster);
        //        return true;//Кластер создан
        //    }
        //    else
        //    {
        //        return false;//Не удалось создать в виду наличия в системе уже существующего кластера
        //    }
        //}

        //public Task<bool> AddNewCluster(string nameClaster, int ParentId)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<bool> AddNewCluster(AddClusterDto clusterDto)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(clusterDto.Name);
            Cluster clusterParent = await _clusterRepository.GetNameCluster(clusterDto.NameParentCluseter);
           
            if (cluster is null && string.IsNullOrEmpty(clusterDto.NameParentCluseter))
            {
                cluster = new Cluster
                {
                    Name = clusterDto.Name,
                    ParentId = -1,
                };
                var result = await _clusterRepository.Add(cluster);
                return true; //Кластер создан
            }

            if (clusterParent is null) 
            {
                return false;
            }
            if (cluster is null)
            {
                cluster = new Cluster
                {
                    Name = clusterDto.Name,
                    ParentId = clusterParent.Id
                };
                var result = await _clusterRepository.Add(cluster);
                return true;//Кластер создан
            }
            else
            {
                return false;//Не удалось создать в виду наличия в системе уже существующего кластера
            }
        }

        //public async Task<bool> DeleteCluster(int clusterId)
        //{
        //    Cluster cluster = await _clusterRepository.Get(clusterId);
        //    if (cluster is null)
        //    {
        //        return false;//Не получилось удалить ввиду отсутствия id кластера
        //    }
        //    else
        //    {
        //        await _clusterRepository.Delete(cluster);
        //        return false; // Кластер удалён
        //    }
        //}

        //public async Task<bool> DeleteCluster(string nameCluster)
        //{
        //    Cluster cluster = await _clusterRepository.GetNameCluster(nameCluster);
        //    if (cluster is null)
        //    {
        //        return false; // Не получилось удалить ввиду отсутствия id кластера
        //    }
        //    else
        //    {
        //        await _clusterRepository.Delete(cluster);
        //        return true; // Кластер удалён

        //    }
        //}

        public async Task<bool> DeleteCluster(DeleteClusterDto deleteClusterDto)
        {
            Cluster cluster = await _clusterRepository.Get(deleteClusterDto.Id);
            if (cluster is null)
            {
                return false; // Не получилось удалить ввиду отсутствия id кластера
            }
            else
            {
                await _clusterRepository.Delete(cluster);
                return true; // Кластер удалён

            }
        }


        //public async Task<List<Cluster>> GetChildrenElementsCluster(int clusterId)
        //{
        //    Cluster cluster = await _clusterRepository.Get(clusterId);
        //    if (cluster is null)
        //    {
        //        //если такого кластера нет
        //        return null;
        //    }
        //    else
        //    {
        //        return await _clusterRepository.GeElementsClaster(clusterId);
        //    }
        //}

        //public async Task<List<Cluster>> GetChildrenElementsCluster(string nameCluster)
        //{
        //    Cluster cluster = await _clusterRepository.GetNameCluster(nameCluster);

        //    if (cluster is null)
        //    {//"Ошибка. Не найден кластер по имени"
        //        return null;
        //    }
        //    return await _clusterRepository.GeElementsClaster(cluster.ClusterId);
        //}

        public async Task<List<ClusterDto>> GetChildrenElementsCluster(GetClusterDto getClusterDto)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(getClusterDto.ClusterName);
            if (cluster is null)
            {//"Ошибка. Не найден кластер по имени"
                return null;
            }
            List<ClusterDto> clusters = new List<ClusterDto>();
            foreach (var item in await _clusterRepository.GeElementsClaster(cluster.Id))
            {

                clusters.Add(Adapters.ClusterAdapter.ConvertFromEntitieToDTO(item));
            }
            return clusters;
        }

      

        //public async Task<bool> UpdateNameCluster(int clasterId, string newNameClaster)
        //{
        //    Cluster cluster = await _clusterRepository.Get(clasterId);
        //    Cluster clusterNewName = await _clusterRepository.GetNameCluster(newNameClaster);

        //    if (cluster is null)
        //    {
        //        return false; //не получилось изменить название классификатора. Не получилось найти указанный кластер в базе
        //    }
        //    else
        //    {
        //        if (clusterNewName is null)
        //        {
        //            cluster.Name = newNameClaster;
        //            await _clusterRepository.Update(cluster);
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
        //    Cluster cluster = await _clusterRepository.GetNameCluster(oldNameClaster);
        //    Cluster clusterNewName = await _clusterRepository.GetNameCluster(newNameClaster);

        //    if (cluster is null)
        //    {
        //        return false;// "не получилось изменить название классификатора. Не получилось найти указанный кластер в базе";
        //    }
        //    else
        //    {

        //        if (clusterNewName is null)
        //        {
        //            cluster.Name = newNameClaster;
        //            await _clusterRepository.Update(cluster);
        //            return true;//"Кластер изменён";
        //        }
        //        else
        //        {
        //            return false;// "не получилось изменить название классификатора. Имя этого кластера занято!";
        //        }
        //    }
        //}

        public async Task<bool> UpdateNameCluster(UpdateCluseterDto updateCluseterDto)
        {
            Cluster cluster = await _clusterRepository.Get(updateCluseterDto.Id);
            Cluster clusterNewName = await _clusterRepository.GetNameCluster(updateCluseterDto.NewName);
            Cluster clusterParent = await _clusterRepository.GetNameCluster(updateCluseterDto.newParent);
            if (cluster is null)
            {
                return false;// "не получилось изменить название классификатора. Не получилось найти указанный кластер в базе";
            }
            else
            {

                if (cluster is not null && string.IsNullOrEmpty(updateCluseterDto.newParent) && clusterNewName is null)
                {
                    cluster.Name = updateCluseterDto.NewName;
                    cluster.ParentId = -1;
                    
                    var result = await _clusterRepository.Update(cluster);
                    return true; //Кластер создан
                }

                if (cluster is not null && clusterParent is not null && clusterNewName is null)
                {
                    cluster.Name = updateCluseterDto.NewName;
                    cluster.ParentId = clusterParent.Id;
                   
                    var result = await _clusterRepository.Update(cluster);
                    return true; //Кластер создан
                }
                    return false;// "не получилось изменить название классификатора. Имя этого кластера занято!";
            }
        }

        //public async Task<bool> UpdatePositionCluster(int clusterId, int parentId)
        //{
        //    Cluster cluster = await _clusterRepository.Get(clusterId);
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
        //            await _clusterRepository.Update(cluster);
        //            return true;// "Изменения были приняты иерархия была изменена";
        //        }

        //        Cluster clusterParent = await _clusterRepository.Get(parentId);
        //        if (clusterParent is not null)
        //        {
        //            cluster.ParentId = parentId;
        //            await _clusterRepository.Update(cluster);
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
        //    Cluster cluster = await _clusterRepository.GetNameCluster(nameCluster);

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
        //            await _clusterRepository.Update(cluster);
        //            return true;// "Изменения были приняты иерархия была изменена";
        //        }

        //        Cluster clusterParent = await _clusterRepository.Get(parentId);
        //        if (clusterParent is not null)
        //        {
        //            cluster.ParentId = parentId;
        //            await _clusterRepository.Update(cluster);
        //            return true;// "Изменения были приняты иерархия была изменена";
        //        }
        //        else
        //        {
        //            return false;//"Ошибка. Родительский кластер не найден!";
        //        }
        //    }
        //}

        public  async Task<List<ClusterDto>>  GetAllElementsCluster()
        {
            List<ClusterDto > clusters = new List<ClusterDto>();

            foreach (var item in (List<Cluster>)await _clusterRepository.GetAll()) 
            {
                clusters.Add(Adapters.ClusterAdapter.ConvertFromEntitieToDTO(item));
            }
            return clusters;
        }

        public async Task<List<ClusterDto>> GetRootElementsCluster()
        {
            List<ClusterDto> clusters = new List<ClusterDto>();
            foreach (var item in await _clusterRepository.GetRootElementsClaster()) 
            {
                clusters.Add(Adapters.ClusterAdapter.ConvertFromEntitieToDTO(item));
            }
            return clusters;
        }
    }
}
