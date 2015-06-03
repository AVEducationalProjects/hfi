using System.Data.Entity.ModelConfiguration;

namespace HFi.Models.Mapping
{
    public class TransactionMap : EntityTypeConfiguration<Transaction>
    {
        public TransactionMap()
        {
            HasOptional(x => x.Category).WithMany(x => x.Transactions).HasForeignKey(x=>x.CategoryId);
        }
    }
}