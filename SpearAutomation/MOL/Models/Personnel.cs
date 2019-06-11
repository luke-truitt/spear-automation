using System;
using System.Collections.Generic;

namespace MOL.Models
{
    public partial class Personnel
    {
        public int MarineId { get; set; }
        public DateTime DateReturning { get; set; }
        public Location Location { get; set; }
        public CertificationLevel CertificationLevel {get;set;}

    }
}
