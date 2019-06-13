using SpearAutomation.Models.Logger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.Spear.Models
{
    public class DashboardModel
    {
        public List<EventLog> logs { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
