using System;
using System.Collections.Generic;

namespace TCPT.Models
{
    public partial class Resource
    {
        public Guid ResourceId { get; set; }
        public int Type { get; set; }
        public bool Available { get; set; }
        public int Location { get; set; }
    }
}
