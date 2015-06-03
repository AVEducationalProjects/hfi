using System.Data.Entity.ModelConfiguration;

namespace HFi.Models.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            HasOptional(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x=>x.ParentId);
        }
    }
}