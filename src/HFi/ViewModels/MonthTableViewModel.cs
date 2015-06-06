using System;
using System.Collections.Generic;
using System.Linq;
using HFi.Models;

namespace HFi.ViewModels
{
    public class MonthTableViewModel 
    {
        public int Year { get; set; }

        public int Month { get; set; }

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

        public decimal this[int day] => this[day, false];

        public decimal this[int day, bool isSum]
        {
            get
            {
                var entries = Entries.Where(x => x.Date.Day == day).ToList();
                return isSum ? entries.Sum(x => x.SumAmount) : entries.Sum(x => x.Amount);
            }
        }


        public decimal this[int day, Category category] => this[day, category, false];

        public decimal this[int day, Category category, bool isSum]
        {
            get
            {
                var entry = Entries.SingleOrDefault(x => x.Category == category && x.Date.Day == day);
                if (entry == null)
                {
                    return 0;
                }
                return isSum ? entry.SumAmount : entry.Amount;
            }
        }

        public MonthTableViewModel(int year, int month, Category rootCategory, List<PlanEntry> planEntries)
        {
            Year = year;
            Month = month;
            RootCategory = rootCategory;
            Entries = planEntries;
        }
    }
}