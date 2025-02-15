using BLL.Abstractions;
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


namespace BLL.Products
{
    public class ClusterService : IClusterService
    {
        private readonly ClusterRepository _clusterRepository;
        public ClusterService(IContextManager contextManager)
        {
            _clusterRepository = new ClusterRepository(contextManager);
        }
      
        public async Task<bool> AddNewCluster(string nameClaster)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(nameClaster);
            if (cluster is null)
            {
                cluster = new Cluster
                {
                    Name = nameClaster,
                    ParentId = -1,
                };
                var result = await _clusterRepository.Add(cluster);
                return true; //Кластер создан 
            }
            else
            {
                return false;   //Не удалось создать в виду наличия в системе уже существующего кластера
            }
        }

        public async Task<bool> AddNewCluster(string nameClaster, int ParentId)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(nameClaster);
            Cluster clusterParent = await _clusterRepository.Get(ParentId);
            if (clusterParent is null)
            {
                return false; // По указанному id не нашёл родителя
            }
            if (cluster is null)
            {
                cluster = new Cluster
                {
                    Name = nameClaster,
                    ParentId = ParentId,
                };
                var result = await _clusterRepository.Add(cluster);
                return true;// "Кластер создан";
            }
            else
            {
                return false;//Не удалось создать в виду наличия в системе уже существующего кластера
            }
        }

        public async Task<bool> AddNewCluster(string nameClaster, string NameParent)
        {
            Cluster cluster = await _clusterRepository.GetNameCluster(nameClaster);
            Cluster clusterParent = await _clusterRepository.GetNameCluster(NameParent);
            if (clusterParent is null)
            {
                return false;//"По указанному имени не нашёл родителя";
            }
            if (cluster is null)
            {
                cluster = new Cluster
                {
                    Name = nameClaster,
                    ParentId = clusterParent.Id
                };
                var result = await _clusterRepository.Add(cluster);
                return "Кластер создан";
            }
            else
            {
                return "Не удалось создать в виду наличия в системе уже существующего кластера";
            }
        }

        public Task<bool> DeleteCluster(int clusterId)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteCluster(string nameCluster)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cluster>> GetAllElementsCluster()
        {
            throw new NotImplementedException();
        }

        public Task<List<Cluster>> GetChildrenElementsCluster(int clusterId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cluster>> GetChildrenElementsCluster(string nameCluster)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cluster>> GetRootElementsCluster()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateNameCluster(int clasterId, string newNameClaster)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateNameCluster(string oldNameClaster, string newNameClaster)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdatePositionCluster(int clusterId, int parentId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdatePositionCluster(string nameCluster, int parentId)
        {
            throw new NotImplementedException();
        }
    }
}
