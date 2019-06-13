using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOL.Models.Services
{
    public interface IMOLService
    {
        List<Personnel> Get();
        Task<List<Personnel>> GetAsync();
        Personnel GetById(int id);
        Personnel Create(Personnel personnel);
        Personnel Update(Personnel personnel);
        void Delete(Personnel personnel);
    }
}
