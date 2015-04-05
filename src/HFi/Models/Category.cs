using System.Collections.Generic;

namespace HFi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category Parent { get; set; }
        public virtual IList<Transaction> Transactions { get; set; }
    }
}