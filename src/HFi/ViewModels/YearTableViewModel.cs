using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HFi.Models;

namespace HFi.ViewModels
{
    public class YearTableViewModel
    {
        public Category RootCategory { get; private set; }

        public List<PlanEntry> Entries { get; private set; }

        public decimal this[Category category] => this[category, false];

        public decimal this[Category category, bool isSum]
        {
            get
            {
                var entries = Entries.Where(x => x.Category == category).ToList();
                return isSum ? entries.Sum(x => x.SumAmount) : entries.Sum(x => x.Amount);
            }
        }

        public decimal this[int month] => this[month, false];

        public decimal this[int month, bool isSum]
        {
            get
            {
                var entries = Entries.Where(x => x.Date.Month == month).ToList();
                return isSum ? entries.Sum(x => x.SumAmount) : entries.Sum(x => x.Amount);
            }
        }


        public decimal this[int month, Category category] => this[month, category, false];

        public decimal this[int month, Category category, bool isSum]
        {
            get
            {
                var entries = Entries.Where(x => x.Category == category && x.Date.Month == month).ToList();
                return isSum ? entries.Sum(x=>x.SumAmount) : entries.Sum(x=>x.Amount);
            }
        }


        public YearTableViewModel(Category rootCategory, List<PlanEntry> planEntries)
        {
            RootCategory = rootCategory;
            Entries = planEntries;
        }
    }
}
