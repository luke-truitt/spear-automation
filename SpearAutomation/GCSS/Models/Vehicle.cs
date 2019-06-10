using System;
using System.Collections.Generic;

namespace GCSS.Models
{
    public partial class Vehicle
    {
        public Guid Tam { get; set; }
        public DateTime DateAvailable { get; set; }
        public int Location { get; set; }
    }
}
