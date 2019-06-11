using SpearAutomation.Models.Enums;
using System;
using System.Collections.Generic;

namespace SpearAutomation.Models.TCPT
{
    public partial class Resource
    {
        public int ResourceId { get; set; }
        public ResourceType Type { get; set; }
        public bool Available { get; set; }
        public Location Location { get; set; }
        public VehicleType? VehicleType { get; set; }
        public CertificationLevel? CertificationLevel { get; set; }
    }
}
