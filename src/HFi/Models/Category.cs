using System.Collections.Generic;

namespace HFi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category Parent { get; set; }
        public int? ParentId { get; set; }
        public virtual IList<Category> Children { get; set; } 
        public virtual IList<Transaction> Transactions { get; set; }

        public Category()
        {
            Children = new List<Category>();
            Transactions = new List<Transaction>();
        }
    }
}