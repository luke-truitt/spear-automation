using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCSS.Models.Repositories
{
    public class GCSSRepository : IGCSSRepository
    {
        private readonly SPEARGCSSContext _context;

        public GCSSRepository(SPEARGCSSContext context)
        {
            _context = context;
        }

        public IQueryable<Vehicle> Get()
        {
            return _context.Vehicle.OrderByDescending(x => x.DateAvailable);
        }

        public Vehicle GetById(int id)
        {
            return _context.Vehicle.FirstOrDefault(x => id==x.Tam);
        }

        public Vehicle Create(Vehicle vehicle)
        {
            _context.Vehicle.Add(vehicle);
            _context.SaveChanges();
            return vehicle;
        }

        public Vehicle Update(Vehicle vehicle)
        {
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
