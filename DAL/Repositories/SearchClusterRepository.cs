using DAL.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SearchClusterRepository : BaseRepository<SearchCluster>
    {
        public SearchClusterRepository(IContextManager manager) : base(manager)
        {

        }

    }
}
