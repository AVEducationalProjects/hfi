namespace HFi.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DataModel : DbContext
    {
        
        public DataModel()
            : base("name=DataModel")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
    }

}