using System;
using System.Collections.Generic;
using System.Linq;

namespace HFi.Models
{
    public class PlanEntry
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual Category Category { get; set; }

        public decimal Amount { get; set; }

        public decimal SumAmount
        {
            get { return Amount + SubCategoryEntries.Sum(x => x.Amount); }
        }

        public List<PlanEntry> SubCategoryEntries { get; set; }

        public PlanEntry()
        {
            SubCategoryEntries =new List<PlanEntry>();
        }
    }
}