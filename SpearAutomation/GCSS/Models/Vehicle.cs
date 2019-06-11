using System;
using System.Collections.Generic;

namespace GCSS.Models
{
    public partial class Vehicle
    {
        public int Tam { get; set; }
        public DateTime DateAvailable { get; set; }
        public Location Location { get; set; }
        public VehicleType VehicleType { get; set; }

        public string ToCSVString()
        {
            return Tam + "," + DateAvailable.ToString() + "," + ((int)Location).ToString() + "," + ((int)VehicleType).ToString() + "\n";
        }
    }
}
