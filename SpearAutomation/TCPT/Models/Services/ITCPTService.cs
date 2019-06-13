using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCPT.Models.Services
{
    public interface ITCPTService
    {
        List<Resource> Get();
        Task<List<Resource>> GetAsync();
        Resource GetById(int id);
        Resource Create(Resource resource);
        Resource Update(Resource resource);
        void Delete(Resource resource);
    }
}
