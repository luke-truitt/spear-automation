using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCSS.Models.Repositories
{
    public interface IGCSSRepository
    {
        IQueryable<Vehicle> Get();
        Vehicle GetById(int id);
        Vehicle Create(Vehicle vehicle);
        Vehicle Update(Vehicle vehicle);
        void Delete(Vehicle vehicle);
    }
}
