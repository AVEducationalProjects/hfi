using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFi.Models.Mapping
{
    class PlanMap : EntityTypeConfiguration<Plan>
    {
        public PlanMap()
        {
            HasMany(x => x.Entries).WithRequired().WillCascadeOnDelete();
        }
    }
}
