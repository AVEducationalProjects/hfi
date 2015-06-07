using System.Collections.Generic;

namespace HFi.Models.Fuzzy
{
    public abstract class Term
    {
        public abstract double Calculate(Transaction transaction);
    }

    public class AmountTerm : Term
    {
        public decimal A1 { get; set; }
        public decimal A2 { get; set; }
        public decimal A3 { get; set; }
        public decimal A4 { get; set; }
        public override double Calculate(Transaction transaction)
        {
            if (transaction.Amount > A1 && transaction.Amount < A2)
                return (double)((transaction.Amount - A1)/(A2 - A1));

            if (transaction.Amount >= A2 && transaction.Amount <= A3)
                return 1;

            if (transaction.Amount > A3 && transaction.Amount < A4)
                return (double)(1-(transaction.Amount - A3) / (A4 - A3));

            return 0;
        }

        public AmountTerm(decimal a1, decimal a2, decimal a3, decimal a4)
        {
            A1 = a1;
            A2 = a2;
            A3 = a3;
            A4 = a4;
        }
    }

    public class MonthTerm : Term
    {
        private readonly Dictionary<int, double> _table = new Dictionary<int, double>(); 
        public override double Calculate(Transaction transaction)
        {
            return _table[transaction.Date.Day];
        }

        public MonthTerm(double[] table)
        {
            for (int i = 0; i < table.Length; i++)
            {
                _table.Add(i+1, table[i]);
            }
        }
    }

    public class WeekTerm : Term
    {
        private readonly Dictionary<int, double> _table = new Dictionary<int, double>();
        public override double Calculate(Transaction transaction)
        {
            return _table[(int)transaction.Date.DayOfWeek];
        }

        public WeekTerm(double [] table)
        {
            for (int i = 0; i < table.Length; i++)
            {
                _table.Add(i+1, table[i]);
            }
        }
    }

    public class SourceTerm : Term
    {
        public SourceCategory SourceCategory { get; private set; }
       
        private readonly Dictionary<string, double> _table = new Dictionary<string, double>();

        public SourceTerm(SourceCategory sourceCategory, Dictionary<string, double> table)
        {
            SourceCategory = sourceCategory;
            _table = table;
        }

        public override double Calculate(Transaction transaction)
        {
            if (_table.ContainsKey(transaction.Source))
                return _table[transaction.Source];
            return 0;
        }
    }
}