using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HFi.Models
{
    public class FuzzyRule
    {
        public int Id { get; set; }

        public string Proposition { get; set; }

        public virtual Category Conclusion { get; set; }

    }
}
