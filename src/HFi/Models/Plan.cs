using System;
using System.Collections.Generic;

namespace HFi.Models
{
    public class Plan
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public DateTime LastChanged { get; set; }

        public virtual IList<PlanEntry> Entries { get; set; }
    }
}
