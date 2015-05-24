using System;

namespace HFi.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public virtual Category Category { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public string Purpose { get; set; }
        public decimal Amount { get; set; }
    }
}