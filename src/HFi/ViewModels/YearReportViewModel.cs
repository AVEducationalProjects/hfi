using System.Collections.Generic;
using System.Linq;
using HFi.Models;

namespace HFi.ViewModels
{
    public class YearReportViewModel
    {
        public int Year { get; set; }

        public Category RootCategory { get; set; }

        private List<Transaction> _transactions;

        public YearReportViewModel(int year, Category rootCategory, List<Transaction> transactions)
        {
            Year = year;
            RootCategory = rootCategory;
            _transactions = transactions;
        }

        public decimal this[int month, Category category]
        {
            get
            {
                return _transactions.Where(x => x.Category == category && x.Date.Month == month).Sum(x => x.Amount);
            }
        }
        public decimal this[Category category]
        {
            get
            {
                return _transactions.Where(x => x.Category == category).Sum(x => x.Amount);
            }
        }

        public decimal this[int month]
        {
            get
            {
                return _transactions.Where(x => x.Date.Month == month).Sum(x => x.Amount);
            }
        }
    }
}