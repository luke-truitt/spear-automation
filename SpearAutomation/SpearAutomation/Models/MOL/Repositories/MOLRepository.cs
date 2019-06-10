using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.MOL.Repositories
{
    public class MOLRepository : IMOLRepository
    {
        private readonly SPEARMOLContext _context;
        private readonly ILogger<MOLRepository> _logger;
        
        public MOLRepository(ILogger<MOLRepository> logger, SPEARMOLContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IEnumerable<Personnel> Get()
        {
            return _context.Personnel;
        }
    }
}
