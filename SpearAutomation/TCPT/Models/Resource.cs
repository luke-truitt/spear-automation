using System;
using System.Collections.Generic;

namespace TCPT.Models
{
    public partial class Resource
    {
        public int ResourceId { get; set; }
        public ResourceType Type { get; set; }
        public bool Available { get; set; }
        public Location Location { get; set; }
        public VehicleType? VehicleType { get; set; }
        public CertificationLevel? CertificationLevel { get; set; }

        public string ToCSVString()
        {
            return ResourceId.ToString() + "," + ((int)Type).ToString() + "," + Available.ToString() + "," + ((int)Location).ToString() + "," + (VehicleType != null ? ((int)VehicleType).ToString() : "") + "," + (CertificationLevel != null ? ((int)CertificationLevel).ToString() : "") + "\n";
         }
    }
}
