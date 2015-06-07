using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFi.Models
{
    public class SourceCategoryToSource
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public virtual SourceCategory SourceCategory { get; set; }
        public virtual Source Source{ get; set; }
    }
}
