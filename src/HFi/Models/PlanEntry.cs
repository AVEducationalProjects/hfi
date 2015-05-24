using System;

namespace HFi.Models
{
    public class PlanEntry
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual Category Category { get; set; }

        public decimal Amount { get; set; }

    }
}