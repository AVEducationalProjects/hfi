using System;
using System.Collections.Generic;
using System.Linq;
using WebGrease.Css.Extensions;

namespace HFi.Models.Fuzzy
{
    public abstract class FuzzyExpression
    {
        public abstract double Calculate(Transaction transaction);
    }

    public abstract class NotAtomicFuzzyExpression : FuzzyExpression
    {
        public IList<FuzzyExpression> Expressions { get; private set; }

        protected NotAtomicFuzzyExpression(FuzzyExpression[] expressions ) 
        {
            Expressions = new List<FuzzyExpression>();
            expressions.ForEach(x=>Expressions.Add(x));
        }
    }

    public class AndExpression : NotAtomicFuzzyExpression
    {
        public override double Calculate(Transaction transaction)
        {
            return Expressions.Min(x => x.Calculate(transaction));
        }

        public AndExpression(FuzzyExpression[] expressions) : base(expressions)
        {
        }

        public override string ToString()
        {
            return "(" + Expressions.Select(x=>x.ToString()).Aggregate((i, j) => i + " И " + j) + ")"; 
        }
    }

    public class OrExpression : NotAtomicFuzzyExpression
    {
        public override double Calculate(Transaction transaction)
        {
            return Expressions.Max(x => x.Calculate(transaction));
        }

        public OrExpression(FuzzyExpression[] expressions) : base(expressions)
        {
        }

        public override string ToString()
        {
            return "(" + Expressions.Select(x => x.ToString()).Aggregate((i, j) => i + " ИЛИ " + j) + ")";
        }
    }

    public class NotExpression : NotAtomicFuzzyExpression
    {
        public override double Calculate(Transaction transaction)
        {
            return Math.Max(0, 1 - Expressions.First().Calculate(transaction));
        }

        public NotExpression(FuzzyExpression[] expressions) : base(expressions)
        {
        }

        public override string ToString()
        {
            return "НЕ " + Expressions.First().ToString();
        }
    }

    public class AtomicFuzzyExpression : FuzzyExpression
    {
        public Term Term { get; private set; }
        public string Name { get; private set; }
        public override double Calculate(Transaction transaction)
        {
            return Term.Calculate(transaction);
        }

        public AtomicFuzzyExpression(string name, Term term)
        {
            Name = name;
            Term = term;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}