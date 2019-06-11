using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOL.Models.Services
{
    public class MOLService : IMOLService
    {
        private readonly SPEARMOLContext _context;

        public MOLService(SPEARMOLContext context)
        {
            _context = context;
        }

        public List<Personnel> Get()
        {
            return _context.Personnel.ToList();
        }

        public Personnel GetById(int id)
        {
            return _context.Personnel.FirstOrDefault(x => x.MarineId == id);
        }

        public Personnel Create(Personnel personnel)
        {
            _context.Database.OpenConnection();
            _context.Personnel.Add(personnel);
            _context.SaveChanges();
            return personnel;
        }

        public Personnel Update(Personnel personnel)
        {
            _context.Database.OpenConnection();
            _context.Personnel.Update(personnel);
            _context.SaveChanges();
            return personnel;
        }

        public void Delete(Personnel personnel)
        { 
            _context.Personnel.Remove(personnel);
            _context.SaveChanges();
        }
    }
}
