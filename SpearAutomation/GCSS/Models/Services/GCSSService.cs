using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCSS.Models.Services
{
    public class GCSSService : IGCSSService
    {
        private readonly SPEARGCSSContext _context;

        public GCSSService(SPEARGCSSContext context)
        {
            _context = context;
        }

        public List<Vehicle> Get()
        {
            return _context.Vehicle.ToList();
        }

        public Vehicle GetById(int id)
        {
            return _context.Vehicle.FirstOrDefault(x => x.Tam == id);
        }

        public Vehicle Create(Vehicle vehicle)
        {
            _context.Database.OpenConnection();
            _context.Vehicle.Add(vehicle);
            _context.SaveChanges();
            return vehicle;
        }

        public Vehicle Update(Vehicle vehicle)
        {
            _context.Database.OpenConnection();
            _context.Vehicle.Update(vehicle);
            _context.SaveChanges();
            return vehicle;
        }

        public void Delete(Vehicle vehicle)
        {
            _context.Vehicle.Remove(vehicle);
            _context.SaveChanges();
        }
    }
}
