using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCPT.Models.Services
{
    public class TCPTService : ITCPTService
    {
        private readonly SPEARTCPTContext _context;

        public TCPTService(SPEARTCPTContext context)
        {
            _context = context;
        }

        public List<Resource> Get()
        {
            return _context.Resource.ToList();
        }

        public async Task<List<Resource>> GetAsync()
        {
            return await _context.Resource.ToListAsync();
        }

        public Resource GetById(int id)
        {
            return _context.Resource.FirstOrDefault(x => x.ResourceId == id);
        }

        public Resource Create(Resource resource)
        {
            _context.Database.OpenConnection();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Resource ON");
            _context.Resource.Add(resource);
            _context.SaveChanges();
            _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Resource OFF");
            return resource;
        }

        public Resource Update(Resource resource)
        {
            _context.Database.OpenConnection();
            _context.Resource.Update(resource);
            _context.SaveChanges();
            return resource;
        }

        public void Delete(Resource personnel)
        {
            _context.Resource.Remove(personnel);
            _context.SaveChanges();
        }
    }
}
