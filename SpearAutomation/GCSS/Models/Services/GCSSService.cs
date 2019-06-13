using GCSS.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GCSS.Models.Services
{
    public class GCSSService : IGCSSService
    {
        private readonly IGCSSRepository _repository;

        public GCSSService(IGCSSRepository repository)
        {
            _repository = repository;
        }

        public List<Vehicle> Get()
        {
            return _repository.Get().ToList();
        }

        public async Task<List<Vehicle>> GetAsync()
        {
            return await _repository.Get().ToListAsync();
        }

        public Vehicle GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Vehicle Create(Vehicle vehicle)
        {
            return _repository.Create(vehicle);
        }

        public Vehicle Update(Vehicle vehicle)
        {
            return _repository.Update(vehicle);
        }

        public void Delete(Vehicle vehicle)
        {
            _repository.Delete(vehicle);
        }
    }
}
