using SpearAutomation.Models.GCSS;
using SpearAutomation.Models.TCPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.Mappers
{
    public static class VehicleMapper
    {
        public static Resource ToResource(this Vehicle vehicle)
        {
            return new Resource {
                ResourceId = vehicle.Tam.GetHashCode(),
                Location = vehicle.Location,
                Type = Enums.ResourceType.Vehicle,
                Available = vehicle.DateAvailable < DateTime.Now,
                VehicleType = vehicle.VehicleType
            };
        
        }
    }
}
