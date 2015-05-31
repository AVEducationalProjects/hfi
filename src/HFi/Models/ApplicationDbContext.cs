using System.Data.Entity;
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

        public System.Data.Entity.DbSet<HFi.Models.Transaction> Transactions { get; set; }

        public System.Data.Entity.DbSet<HFi.Models.Rule> Rules { get; set; }
    }
}