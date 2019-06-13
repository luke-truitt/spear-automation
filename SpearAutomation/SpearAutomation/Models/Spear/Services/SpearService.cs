using Microsoft.Extensions.Logging;
using SpearAutomation.Models.GCSS.Repositories;
using SpearAutomation.Models.Mappers;
using SpearAutomation.Models.MOL.Repositories;
using SpearAutomation.Models.TCPT;
using SpearAutomation.Models.TCPT.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.Spear.Services
{
    public class SpearService : ISpearService
    {
        private readonly IGCSSRepository _gcssRepository;
        private readonly IMOLRepository _molRepository;
        private readonly ITCPTRepository _tcptRepository;
        private readonly ILogger<SpearService> _logger;
        public static DateTime LastRead;

        public SpearService(IGCSSRepository gcssRepository, IMOLRepository molRepository, ITCPTRepository tcptRepository, ILogger<SpearService> logger)
        {
            _gcssRepository = gcssRepository;
            _molRepository = molRepository;
            _tcptRepository = tcptRepository;
            _logger = logger;
            if(LastRead == null)
            {
                LastRead = DateTime.Now;
            }
        }

        public void Update()
        {
            var vehicles = _gcssRepository.Get().Select(x => x.ToResource()).ToList();
            var marines = _molRepository.Get().Select(y => y.ToResource()).ToList();
            _tcptRepository.UpdateList(vehicles);
            _tcptRepository.UpdateList(marines);
            LastRead = DateTime.Now;
        }
    }
}
