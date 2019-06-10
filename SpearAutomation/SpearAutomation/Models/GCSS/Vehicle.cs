using SpearAutomation.Models.Enums;
using System;
using System.Collections.Generic;

namespace SpearAutomation.Models.GCSS
{
    public partial class Vehicle
    {
        public int Tam { get; set; }
        public DateTime DateAvailable { get; set; }
        public Location Location { get; set; }
    }
}
