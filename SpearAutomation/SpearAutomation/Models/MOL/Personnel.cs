using SpearAutomation.Models.Enums;
using System;
using System.Collections.Generic;

namespace SpearAutomation.Models.MOL
{
    public partial class Personnel
    {
        public int MarineId { get; set; }
        public DateTime DateReturning { get; set; }
        public Location Location { get; set; }
    }
}
