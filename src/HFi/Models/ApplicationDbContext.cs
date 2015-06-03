using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using HFi.Models.Mapping;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HFi.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SourceCategory> SourceCategories { get; set; }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TransactionMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<HFi.Models.Transaction> Transactions { get; set; }

        public System.Data.Entity.DbSet<HFi.Models.FuzzyRule> Rules { get; set; }
    }
}