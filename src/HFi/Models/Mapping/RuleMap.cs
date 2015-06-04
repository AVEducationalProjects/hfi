using System.Data.Entity.ModelConfiguration;

namespace HFi.Models.Mapping
{
    public class RuleMap : EntityTypeConfiguration<FuzzyRule>
    {
        public RuleMap()
        {
            HasRequired(x => x.Conclusion).WithMany().HasForeignKey(x => x.ConclusionId);
            Ignore(x => x.PropositionExpression);
        }
    }
}