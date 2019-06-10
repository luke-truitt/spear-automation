using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.TCPT.Repositories
{
    public interface ITCPTRepository
    {
        IEnumerable<Resource> Get();

        Resource GetById(int id);

        Resource Create(Resource entity);

        Resource Update(Resource resource);

        IEnumerable<Resource> UpdateList(IEnumerable<Resource> resources);
    }
}
