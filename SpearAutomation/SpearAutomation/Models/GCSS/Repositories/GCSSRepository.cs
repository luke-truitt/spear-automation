using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.GCSS.Repositories
{
    public class GCSSRepository : IGCSSRepository
    {
        private readonly SPEARGCSSContext _context;
        private readonly ILogger<GCSSRepository> _logger;

        public GCSSRepository(ILogger<GCSSRepository> logger, SPEARGCSSContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<Vehicle> Get()
        {
            return _context.Vehicle;
        }
    }
}
