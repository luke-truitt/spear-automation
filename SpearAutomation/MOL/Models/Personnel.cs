using System;
using System.Collections.Generic;

namespace MOL.Models
{
    public partial class Personnel
    {
        public Guid MarineId { get; set; }
        public DateTime DateReturning { get; set; }
        public int Location { get; set; }
    }
}
