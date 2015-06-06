using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFi.Models.Mapping
{
    class PlanEntryMap : EntityTypeConfiguration<PlanEntry>
    {
        public PlanEntryMap()
        {
            Ignore(x => x.SubCategoryEntries);
            Ignore(x => x.SumAmount);
        }
    }
}
