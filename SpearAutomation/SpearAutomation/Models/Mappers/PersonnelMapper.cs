using SpearAutomation.Models.MOL;
using SpearAutomation.Models.TCPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpearAutomation.Models.Mappers
{
    public static class PersonnelMapper
    {
        public static Resource ToResource(this Personnel personnel)
        {
            return new Resource
            {
                ResourceId = personnel.MarineId.GetHashCode(),
                Location = personnel.Location,
                Type = Enums.ResourceType.Personnel,
                Available = personnel.DateReturning < DateTime.Now
            };
        }
    }
}
