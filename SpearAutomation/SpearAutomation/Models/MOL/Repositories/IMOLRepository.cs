using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.MOL.Repositories
{
    public interface IMOLRepository
    {
        IEnumerable<Personnel> Get();
    }
}
