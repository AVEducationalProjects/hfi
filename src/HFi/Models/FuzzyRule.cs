using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HFi.Controllers;
using HFi.Models.Fuzzy;

namespace HFi.Models
{
    public class FuzzyRule
    {
        public int Id { get; set; }

        public string Proposition { get; set; }

        public FuzzyExpression PropositionExpression { get; set; }

        public virtual Category Conclusion { get; set; }

        public int ConclusionId { get; set; }

        public FuzzyRule()
        {
            Proposition = @"{""type"":""and"", ""expressions"":[]}";
        }

        public void BuildPropositionExpression(RuleBuilder ruleBuilder)
        {
            PropositionExpression = ruleBuilder.Build(Proposition);
        }

        public override string ToString()
        {
            if (PropositionExpression != null)
                return PropositionExpression.ToString();
            return base.ToString();
        }
    }
}

