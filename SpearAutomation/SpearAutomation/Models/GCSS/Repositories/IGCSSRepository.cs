using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.GCSS.Repositories
{
    public interface IGCSSRepository
    {
        IEnumerable<Vehicle> Get();
    }
}
